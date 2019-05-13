namespace NudgeApp.Web.Controllers
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataAnalysis.API;
    using System;
    using System.Linq;

    [Route("[controller]/[action]")]
    public class AnalysisController : Controller
    {
        private readonly IAnalyzer Analyzer;

        public AnalysisController(IAnalyzer analyzer)
        {
            this.Analyzer = analyzer;
        }

        [HttpGet]
        public IActionResult Analyze()
        {
            var result = this.Analyzer.AnalyseWeather();

            return this.Ok(result);
        }

        [Authorize]
        [HttpPost]
        public IActionResult AnalyseEvent([FromBody]  AnalyseEventRequest analyseEventRequest)
        {
            var userId = Guid.Parse(HttpContext.User.Identities.First().Name);

            this.Analyzer.AnalyseEvent(userId, analyseEventRequest.UserEvent, analyseEventRequest.UserCoordinates);

            return this.Ok();
        }
    }

    public class AnalyseEventRequest
    {
        public UserEvent UserEvent { get; set; }
        public Coordinates UserCoordinates { get; set; }
    }
}
