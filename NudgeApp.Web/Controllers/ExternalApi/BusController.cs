namespace NudgeApp.Web.Controllers.ExternalApi
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Bus.BusStop;

    [Route("[controller]/[action]")]
    public class BusController : Controller
    {
        private ITripSearch TripSearch { get; set; }

        public BusController(ITripSearch tripSearch)
        {
            this.TripSearch = tripSearch;
        }

        [HttpGet]
        public ActionResult<TripObject> GetBusTrip(string from, string to, DateTime? dateTime, TripSchedule? tripSchedule)
        {
            var result = this.TripSearch.SearchTrip(from, to, dateTime, tripSchedule);
            return this.Ok(result);
        }

        [HttpGet]
        public ActionResult<Stages> GetNearestStop()
        {
            var result = this.TripSearch.NearestStops(0, 0);
            return this.Ok(result);
        }
    }
}
