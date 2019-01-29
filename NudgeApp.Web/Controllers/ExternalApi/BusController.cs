namespace NudgeApp.Web.Controllers.ExternalApi
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.DataManagement.ExternalApi.Bus;

    [Route("externalAPI/Bus")]
    public class BusController : Controller
    {
        private ITripSearch TripSearch { get; set; }

        public BusController(ITripSearch tripSearch)
        {
            this.TripSearch = tripSearch;
        }

        [HttpGet]
        [Route("getBusTrip")]
        public ActionResult<TripObject> GetBusTrip(string from, string to, DateTime? dateTime, TripSchedule? tripSchedule)
        {
            var result = this.TripSearch.SearchTrip(from, to, dateTime, tripSchedule);
            return this.Ok(result);
        }
    }
}
