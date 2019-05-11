namespace NudgeApp.Web.Controllers
{
    using System;
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    [Route("[controller]/[action]")]
    public class PreferencesController : Controller
    {
        private readonly IUserService UserService;

        public PreferencesController(IUserService userService)
        {
            this.UserService = userService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult Set(PreferencesDto preferences)
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            this.UserService.UpdatePreferences(userId, preferences);

            return this.Ok();
        }

        [HttpGet]
        [Authorize]

        public ActionResult<PreferencesDto> Get()
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            var result = this.UserService.GetPreferences(userId);

            return this.Ok(result);
        }
    }
}
