using Microsoft.AspNetCore.Mvc;
using NudgeApp.DataAnalysis.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NudgeApp.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AnalysisController : Controller
    {
        private readonly IAnalyzer Analyzer;

        public AnalysisController(IAnalyzer analyzer)
        {
            this.Analyzer = analyzer;
        }

        [HttpGet]
        public IActionResult Analyze ()
        {
            var result = this.Analyzer.AnalyseWeather();

            return this.Ok(result);
        }

        [HttpPost]
        public IActionResult GetEvent([FromBody] Event ev)
        {
            
            // this.AnalysisService.Analyze(event)
            Console.WriteLine(ev);

            return this.Ok();
        }
    }

    public class Event
    {
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
    }
}
