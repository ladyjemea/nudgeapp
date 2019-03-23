namespace NudgeApp.Web.Controllers.ExternalApi
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
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
            var result = this.BusService.SearchTrip(from, to, out _, dateTime, tripSchedule);
            return this.Ok(result);
        }

        [HttpGet]
        public ActionResult<Stages> GetNearestStop()
        {
            var result = this.BusService.NearestStops(new Coordinates() { Latitude = 0, Longitude = 0 });
            return this.Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<BusTripDto>> GetTrip([FromBody] TravelObject travelObject)
        {
            /*   travelObject.From = new Coordinates
               {
                   Latitude = 69.6801,
                   Longitude = 18.97
               };
               travelObject.To = new Coordinates
               {
                   Latitude = 69.628801,
                   Longitude = 18.915912
               };
               //travelObject.When = new DateTime(2019, 03, 09, 21, 00, 00);*/

            var result = await this.BusService.FindBusTrip(travelObject.From, travelObject.To, travelObject.When, travelObject.Schedule);

            return this.Ok(result);
        }
    }

    public class TravelObject
    {
        public Coordinates From { get; set; }
        public Coordinates To { get; set; }
        public DateTime When { get; set; }
        public TripSchedule Schedule { get; set; }
    }
}
