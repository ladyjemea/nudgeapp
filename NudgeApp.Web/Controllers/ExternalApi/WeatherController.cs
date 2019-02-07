namespace NudgeApp.Web.Controllers.ExternalApi
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.DataManagement.ExternalApi.Weather;

    [Route("externalAPI/Weather")]
    public class WeatherController : Controller
    {
        private IWeatherApi WeatherApi { get; set; }

        public WeatherController(IWeatherApi weatherApi)
        {
            this.WeatherApi = weatherApi;
        }

        [HttpGet]
        [Route("get12HourWeather")]
        public ActionResult<IList<HourlyForecast>> Get12hWeather()
        {
            var result = this.WeatherApi.Get12HTromsWeather();
            return this.Ok(result);
        }

        [HttpGet]
        [Route("get24HourWeather")]
        public ActionResult<IList<HourlyForecast>> Get24hWeather()
        {
            var result = this.WeatherApi.Get24HTromsWeather();
            return this.Ok(result);
        }
    }
}
