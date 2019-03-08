namespace NudgeApp.Web.Controllers.ExternalApi
{
    using System;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects.BusStop;

    [Route("[controller]/[action]")]
    public class BusController : Controller
    {
        private IBusService TripSearch { get; set; }

        public BusController(IBusService tripSearch)
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
            var result = this.TripSearch.NearestStops(new Coordinates() {Latitude = 0, Longitude = 0});
            return this.Ok(result);
        }
    }
}
