namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Linq;

    [Route("Api/Nudge")]
    public class NudgeController : Controller
    {
        private readonly INudgeLogic NudgeLogic;

        public NudgeController(INudgeLogic nudgeLogic)
        {
            this.NudgeLogic = nudgeLogic;
        }

        [HttpGet]
        [Authorize]
        [Route("addNudge")]
        public IActionResult AddNudge(NudgeDto nudge, ForecastDto forecast, TripDto trip)
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            this.NudgeLogic.AddNudge(userId, nudge, forecast, trip);

            return this.Ok();
        }

        [HttpGet]
        [Route("test")]
        public IActionResult Test()
        {
            this.NudgeLogic.Test();
            return this.Ok();
        }
    }
}
