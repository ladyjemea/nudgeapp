using Microsoft.AspNetCore.Mvc;
using NudgeApp.DataAnalysis.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NudgeApp.Web.Controllers
{
    [Route("Api/Analyzer")]
    public class AnalysisController : Controller
    {
        private readonly IAnalyzer Analyzer;

        public AnalysisController(IAnalyzer analyzer)
        {
            this.Analyzer = analyzer;
        }

        [HttpGet]
        [Route("anlyze")]
        public IActionResult Analyze ()
        {
            var result = this.Analyzer.AnalyseWeather();

            return this.Ok(result);
        }
    }
}
