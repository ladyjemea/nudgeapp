namespace NudgeApp.DataManagement.ExternalApi.Bus
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml.Serialization;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects.BusStop;
    using RestSharp;

    public class BusService : IBusService
    {
        private readonly string tromskortetLink = "https://www.tromskortet.no/reiseplanlegger/category245.html";

        /// <summary>
        /// Searches for trips between 2 stops.
        /// </summary>
        /// <param name="from"> The starting bust stop. </param>
        /// <param name="to"> The ending bus stop. </param>
        /// <param name="dateTime"> The time for departure/arrival. Can be left unset. If unset or null DateTime.Now will be set. </param>
        /// <param name="tripSchedule"> Sets if the dateTime set is for arrival or departure. Departure is default value if not set. </param>
        /// <returns> A list of all trips and details about each one. </returns>
        public TripObject SearchTrip(string from, string to, out string link, DateTime? dateTime = null, TripSchedule? tripSchedule = TripSchedule.Departure)
        {
            var date = (dateTime ?? DateTime.Now).ToShortDateString().Replace('/', '.');
            var time = (dateTime ?? DateTime.Now).ToShortTimeString();
            var client = new RestClient("http://rp.tromskortet.no");
            var request = new RestRequest("scripts/TravelMagic/TravelMagicWE.dll/v1SearchXML", Method.GET);
            request.AddParameter("from", from);
            request.AddParameter("to", to);
            request.AddParameter("date", date);
            request.AddParameter("time", time);
            request.AddParameter("direction", (int)(tripSchedule ?? TripSchedule.Departure));
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; resp.ContentEncoding = "UTF-8"; };

            var response = client.Execute<TripObject>(request);

            link = tromskortetLink + response.ResponseUri.Query;

            TripObject result = null;
            var serializer = new XmlSerializer(typeof(TripObject));
            using (TextReader reader = new StringReader(response.Content))
            {
                result = (TripObject)serializer.Deserialize(reader);
            }

            return result;
        }

        public Stages NearestStops(Coordinates coord)
        {
            var client = new RestClient("http://rp.tromskortet.no");
            var request = new RestRequest("scripts/TravelMagic/TravelMagicWE.dll/v1NearestStopsXML", Method.GET);
            request.AddParameter("x", coord.Longitude);
            request.AddParameter("y", coord.Latitude);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; resp.ContentEncoding = "UTF-8"; };

            var response = client.Execute<Stages>(request);

            Stages result = null;
            var serializer = new XmlSerializer(typeof(Stages));
            using (TextReader reader = new StringReader(response.Content))
            {
                result = (Stages)serializer.Deserialize(reader);
            }

            return result;
        }

        public async Task<BusTripDto> FindBusTrip(Coordinates from, Coordinates to, DateTime travelTime, TripSchedule schedule)
        {
            var nearestStops = this.NearestStops(from);
            var destinationStop = this.NearestStops(to);

            var destinationStopCoordinates = new Coordinates
            {
                Latitude = Convert.ToDouble(destinationStop.Group.First().Y.Replace(',', '.'), CultureInfo.InvariantCulture),
                Longitude = Convert.ToDouble(destinationStop.Group.First().X.Replace(',', '.'), CultureInfo.InvariantCulture)
            };

            var walkToDestination = await this.WalkInfo(destinationStopCoordinates, to);
            var walkToDestinationtDuration = walkToDestination.rows.First()?.elements.First()?.duration.value ?? 0;

            var busArrivalTime = travelTime.AddSeconds((-1) * walkToDestinationtDuration);

            BusTripDto busTrip = null;
            foreach (var departureStop in nearestStops.Group)
            {
                try
                {
                    busTrip = this.GetBusTrip(departureStop, destinationStop.Group.First(), busArrivalTime, schedule);
                    if (busTrip != null)
                        break;
                }
                catch
                {
                }
            }

            if (busTrip == null || busTrip.TravelParts.Count == 0)
                return busTrip;

            var walkToStart = await this.WalkInfo(from, busTrip.StartCoordinates);
            var walkToStartDuration = walkToStart.rows.FirstOrDefault()?.elements.FirstOrDefault()?.duration.value ?? 0;

            if (walkToStartDuration > 0)
            {
                busTrip.TravelParts.Add(0,
                    new TravelPart
                    {
                        DepartureName = "Current Location",
                        ArrivalName = busTrip.TravelParts[1].DepartureName,
                        Duration = TimeSpan.FromSeconds(walkToStartDuration),
                        Type = TransportationType.Walk
                    });

                busTrip.Duration = busTrip.Duration.Add(TimeSpan.FromSeconds(walkToStartDuration));
                busTrip.Start = busTrip.Start.AddSeconds(-1 * walkToStartDuration);
            }

            if (walkToDestinationtDuration > 0)
            {
                busTrip.TravelParts.Add(busTrip.TravelParts.Count + 1,
                    new TravelPart
                    {
                        DepartureName = busTrip.TravelParts[busTrip.TravelParts.Count - 1].ArrivalName,
                        ArrivalName = "Destination",
                        Duration = TimeSpan.FromSeconds(walkToDestinationtDuration),
                        Type = TransportationType.Walk
                    });

                busTrip.Duration = busTrip.Duration.Add(TimeSpan.FromSeconds(walkToDestinationtDuration));
                busTrip.Stop = busTrip.Stop.AddSeconds(walkToDestinationtDuration);
            }

            busTrip.DurationString = "";
            if (busTrip.Duration.Hours > 0)
            {
                busTrip.DurationString += busTrip.Duration.Hours.ToString();
                busTrip.DurationString += busTrip.Duration.Hours == 1 ? " Hour" : " Hours";
                busTrip.DurationString += " and ";
            }

            busTrip.DurationString += busTrip.Duration.Minutes.ToString();
            busTrip.DurationString += busTrip.Duration.Minutes == 1 ? " Minute" : " Minutes";


            return busTrip;
        }

        private BusTripDto GetBusTrip(Group from, Group to, DateTime travelTime, TripSchedule schedule)
        {
            string link = string.Empty;
            var trip = this.SearchTrip(from.N, to.N, out link, travelTime, schedule).Trips.Trip.First();

            var allStops = trip.I.Where(i => i.N != String.Empty && i.N != i.N2).ToList();

            var stops = allStops.Where(i => i.N2 != String.Empty);

            var busTripDto = new BusTripDto
            {
                Start = Convert.ToDateTime(trip.Start),
                Stop = Convert.ToDateTime(trip.Stop),
                Duration = TimeSpan.FromMinutes(Convert.ToInt64(trip.Duration)),
                ChangeNb = Convert.ToInt32(trip.Changecount),
                Link = link,
                StartCoordinates = new Coordinates
                {
                    Latitude = Convert.ToDouble(stops.First().Y.Replace(',', '.'), CultureInfo.InvariantCulture),
                    Longitude = Convert.ToDouble(stops.First().X.Replace(',', '.'), CultureInfo.InvariantCulture)
                },
                EndCoordinates = new Coordinates
                {
                    Latitude = Convert.ToDouble(allStops.Last().Y.Replace(',', '.'), CultureInfo.InvariantCulture),
                    Longitude = Convert.ToDouble(allStops.Last().X.Replace(',', '.'), CultureInfo.InvariantCulture)
                }
            };

            int counter = 1;
            foreach (var stop in stops)
            {
                busTripDto.TravelParts.Add(counter, new TravelPart
                {
                    ArrivalName = stop.N2,
                    DepartureName = stop.N,
                    Type = stop.Tn == "Buss" ? TransportationType.Bus : TransportationType.Walk,
                    Duration = Convert.ToDateTime(stop.A) - Convert.ToDateTime(stop.D)
                });
                counter++;
            }

            return busTripDto;
        }

        private async Task<RootObject> WalkInfo(Coordinates from, Coordinates to)
        {
            var client = new RestClient("https://maps.googleapis.com");
            var request = new RestRequest("maps/api/distancematrix/json", Method.GET);
            request.AddParameter("units", "metric");
            request.AddParameter("origins", from.Latitude.ToString() + ',' + from.Longitude.ToString());
            request.AddParameter("destinations", to.Latitude.ToString() + ',' + to.Longitude.ToString());
            request.AddParameter("mode", "walking");
            request.AddParameter("key", "AIzaSyCCbgVPkBgwul0cofmo-VSMOefNSzrAOEo");
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; resp.ContentEncoding = "UTF-8"; };

            var taskCompletionSource = new TaskCompletionSource<RootObject>();
            client.ExecuteAsync<RootObject>(request, response => taskCompletionSource.SetResult(response.Data));

            return await taskCompletionSource.Task;
        }
    }
}