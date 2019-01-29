namespace NudgeApp.DataManagement.ExternalApi.Bus
{
    using System;
    using System.IO;
    using System.Xml.Serialization;
    using RestSharp;

    class TripSearch : ITripSearch
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
    }
}