namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Linq;

    [Route("[controller]/[action]")]
    public class NudgeController : Controller
    {
        private readonly INudgeLogic NudgeLogic;

        public NudgeController(INudgeLogic nudgeLogic)
        {
            this.NudgeLogic = nudgeLogic;
        }

        [HttpPost]
        [Authorize]
        public IActionResult AddNudge([FromBody] NudgeData nudgeData)
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            this.NudgeLogic.AddNudge(userId, nudgeData);

            return this.Ok();
        }

        [HttpGet]
        public IActionResult Test()
        {
            this.NudgeLogic.Test();
            return this.Ok();
        }
    }
}
