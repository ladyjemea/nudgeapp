namespace NudgeApp.Web.Controllers.ExternalApi
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects.BusStop;

    [Route("[controller]/[action]")]
    public class BusController : Controller
    {
        private IBusService BusService { get; set; }

        public BusController(IBusService busService)
        {
            this.BusService = busService;
        }

        [HttpGet]
        public ActionResult<TripObject> GetBusTrip(string from, string to, DateTime? dateTime, TripSchedule? tripSchedule)
        {
            var result = this.BusService.SearchTrip(from, to, dateTime, tripSchedule);
            return this.Ok(result);
        }

        [HttpGet]
        public ActionResult<Stages> GetNearestStop()
        {
            var result = this.BusService.NearestStops(new Coordinates() {Latitude = 0, Longitude = 0});
            return this.Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<BusTripDto>> GetTrip()
        {
            var from = new Coordinates
            {
                Latitude = 69.6801,
                Longitude = 18.97
            };
            var to = new Coordinates
            {
                Latitude = 69.628801,
                Longitude = 18.915912
            };

            var result = await this.BusService.FindBusTrip(from, to, new DateTime(2019, 03, 09, 21, 00, 00));

            return this.Ok(result);
        }
    }
}
