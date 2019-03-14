﻿namespace NudgeApp.Web.Controllers.ExternalApi
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;

    [Route("[controller]/[action]")]
    public class WeatherController : Controller
    {
        private IWeatherApi WeatherApi { get; set; }

        public WeatherController(IWeatherApi weatherApi)
        {
            this.WeatherApi = weatherApi;
        }

        [HttpGet]
        public ActionResult<IList<HourlyForecast>> Get12hWeather()
        {
            var result = this.WeatherApi.Get12HTromsWeather();
            return this.Ok(result);
        }

        [HttpGet]
        public ActionResult<IList<HourlyForecast>> Get24hWeather()
        {
            var result = this.WeatherApi.Get24HTromsWeather();
            return this.Ok(result);
        }

        [HttpPost]
        public ActionResult<ForecastDto> GetForecast([FromBody] DateTime dateTime)
        {
            var result = this.WeatherApi.GetForecast(dateTime);

            return this.Ok(result);
        }

        [HttpGet]
        public async Task<ActionResult<ForecastDto>> GetCurrentForecast()
        {
            var result = await this.WeatherApi.GetCurrentForecast();

            return this.Ok(result);
        }
    }
}
