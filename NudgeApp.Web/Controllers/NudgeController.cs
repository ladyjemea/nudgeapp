namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataManagement.ExternalApi.Calendar;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Route("[controller]/[action]")]
    public class NudgeController : Controller
    {
        private readonly INudgeService NudgeLogic;
        private readonly IPublicCalendarsService CalendarService;

        public NudgeController(INudgeService nudgeLogic, IPublicCalendarsService calendarService)
        {
            this.NudgeLogic = nudgeLogic;
            this.CalendarService = calendarService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddNudge([FromBody] NudgeData nudgeData)
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

             this.NudgeLogic.AddNudge(userId, nudgeData.NudgeResult, nudgeData.Forecast, nudgeData.Trip);
             
            return this.Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Test()
        {
            await this.CalendarService.GetEvents();

            return this.Ok();
        }
    }

    public class NudgeData
    {
        public NudgeResult NudgeResult { get; set; }
        public WeatherDto Forecast { get; set; }
        public TripDto Trip { get; set; }
    }
}
