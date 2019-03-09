namespace NudgeApp.DataManagement.ExternalApi.Bus
{
    using System;
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
        /// <summary>
        /// Searches for trips between 2 stops.
        /// </summary>
        /// <param name="from"> The starting bust stop. </param>
        /// <param name="to"> The ending bus stop. </param>
        /// <param name="dateTime"> The time for departure/arrival. Can be left unset. If unset or null DateTime.Now will be set. </param>
        /// <param name="tripSchedule"> Sets if the dateTime set is for arrival or departure. Departure is default value if not set. </param>
        /// <returns> A list of all trips and details about each one. </returns>
        public TripObject SearchTrip(string from, string to, DateTime? dateTime = null, TripSchedule? tripSchedule = TripSchedule.Departure)
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

            TripObject result = null;

            var serializer = new XmlSerializer(typeof(TripObject));
            using (TextReader reader = new StringReader(response.Content))
            {
                result = (TripObject)serializer.Deserialize(reader);
            }

            return result;
        }

        public Task<Stages> NearestStops(Coordinates coord)
        {
            var client = new RestClient("http://rp.tromskortet.no");
            var request = new RestRequest("scripts/TravelMagic/TravelMagicWE.dll/v1NearestStopsXML", Method.GET);
            request.AddParameter("x", coord.Longitude);
            request.AddParameter("y", coord.Latitude);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/xml"; resp.ContentEncoding = "UTF-8"; };

            var taskCompletionSource = new TaskCompletionSource<Stages>();
            client.ExecuteAsync<Stages>(request, response => taskCompletionSource.SetResult(response.Data));

            /* Stages result = null;

             var serializer = new XmlSerializer(typeof(Stages));
             using (TextReader reader = new StringReader(response.Content))
             {
                 result = (Stages)serializer.Deserialize(reader);
             }

             Console.WriteLine(result);
             */
            return taskCompletionSource.Task;
        }

        public async Task<BusTripDto> FindBusTrip(Coordinates from, Coordinates to, DateTime arrivalTime)
        {
            var nearestStops = await this.NearestStops(from);
            var destinationStop = await this.NearestStops(to);

            var destinationStopCoordinates = new Coordinates
            {
                Latitude = Convert.ToDouble(destinationStop.Group.First().Y.Replace(',','.')),
                Longitude = Convert.ToDouble(destinationStop.Group.First().X.Replace(',', '.'))
            };

            var walkToDestination = await this.WalkInfo(destinationStopCoordinates, to);

            var busArrivalTime = arrivalTime.AddSeconds((-1) * walkToDestination.rows.First().elements.First().duration.value);

            var bustTrip = this.GetBusTrip(nearestStops.Group.First(), destinationStop.Group.First(), busArrivalTime);

            var walkToStart = await this.WalkInfo(from, bustTrip.StartCoordinates);

            bustTrip.TravelParts.Add(0, new TravelPart {DepartureName = "Current Location", ArrivalName = bustTrip.TravelParts[1].DepartureName, Duration = TimeSpan.FromSeconds(walkToStart.rows.First().elements.First().duration.value), Type = TransportationType.Walk });
            bustTrip.TravelParts.Add(bustTrip.TravelParts.Count + 1, new TravelPart {DepartureName = bustTrip.TravelParts[bustTrip.TravelParts.Count - 1].ArrivalName, ArrivalName = "Destination", Duration = TimeSpan.FromSeconds(walkToDestination.rows.First().elements.First().duration.value), Type = TransportationType.Walk });

            bustTrip.Duration = bustTrip.Duration.Add(TimeSpan.FromSeconds(walkToStart.rows.First().elements.First().duration.value));
            bustTrip.Duration = bustTrip.Duration.Add(TimeSpan.FromSeconds(walkToDestination.rows.First().elements.First().duration.value));

            bustTrip.Start = bustTrip.Start.AddSeconds(-1 * walkToStart.rows.First().elements.First().duration.value);
            bustTrip.Stop = bustTrip.Stop.AddSeconds(walkToDestination.rows.First().elements.First().duration.value);

            return bustTrip;
        }

        private BusTripDto GetBusTrip(Group from, Group to, DateTime arrivalTime)
        {
            var trip = this.SearchTrip(from.N, to.N, arrivalTime, TripSchedule.Arival).Trips.Trip.First();

            var allStops = trip.I.Where(i => i.N != String.Empty && i.N != i.N2).ToList();

            if (allStops.First().Tn == "Gange")
            {
                trip = this.SearchTrip(allStops.First().N2, to.N, arrivalTime, TripSchedule.Arival).Trips.Trip.First();
                allStops = trip.I.Where(i => i.N != String.Empty && i.N != i.N2).ToList();
            }

            var stops = allStops.Where(i => i.N2 != String.Empty);

            var busTripDto = new BusTripDto
            {
                Start = Convert.ToDateTime(trip.Start),
                Stop = Convert.ToDateTime(trip.Stop),
                Duration = TimeSpan.FromMinutes(Convert.ToInt64(trip.Duration)),
                ChangeNb = Convert.ToInt32(trip.Changecount),
                StartCoordinates = new Coordinates
                {
                    Latitude = Convert.ToDouble(stops.First().Y.Replace(',', '.')),
                    Longitude = Convert.ToDouble(stops.First().X.Replace(',', '.'))
                },
                EndCoordinates = new Coordinates
                {
                    Latitude = Convert.ToDouble(allStops.Last().Y.Replace(',', '.')),
                    Longitude = Convert.ToDouble(allStops.Last().X.Replace(',', '.'))
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