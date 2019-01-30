namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    [Route("Api/Nudge")]
    public class NudgeController : Controller
    {
        private readonly INudgeLogic NudgeLogic;

        public NudgeController(INudgeLogic nudgeLogic)
        {
            this.NudgeLogic = nudgeLogic;
        }

        [HttpGet]
        [Route("addNudge")]
        public IActionResult AddNudge(NudgeDto nudge, EnvironmelntalInfoDto envInfo, string userName)
        {
            this.NudgeLogic.AddNudge(nudge, envInfo, userName);
            return this.Ok();
        }
    }
}
