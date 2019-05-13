namespace NudgeApp.DataManagement.ExternalApi.Travel
{
    using System.Threading.Tasks;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects;
    using RestSharp;

    public class WalkService : IWalkService
    {

        public async Task<RootObject> WalkInfo(Coordinates from, Coordinates to)
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
        public async Task<RootObject> WalkInfo(Coordinates from, string to)
        {
            var client = new RestClient("https://maps.googleapis.com");
            var request = new RestRequest("maps/api/distancematrix/json", Method.GET);
            request.AddParameter("units", "metric");
            request.AddParameter("origins", from.Latitude.ToString() + ',' + from.Longitude.ToString());
            request.AddParameter("destinations", to);
            request.AddParameter("mode", "walking");
            request.AddParameter("key", "AIzaSyCCbgVPkBgwul0cofmo-VSMOefNSzrAOEo");
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; resp.ContentEncoding = "UTF-8"; };

            var taskCompletionSource = new TaskCompletionSource<RootObject>();
            client.ExecuteAsync<RootObject>(request, response => taskCompletionSource.SetResult(response.Data));

            return await taskCompletionSource.Task;
        }
    }
}
