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
        [Route("getHourlyWeather")]
        public ActionResult<IList<HourlyForecast>> GetWeather()
        {
            var result = this.WeatherApi.GetTromsWeather();
            return this.Ok(result);
        }
    }
}
