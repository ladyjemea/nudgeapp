namespace NudgeApp.DataManagement.ExternalApi.Weather.Interfaces
{
    using NudgeApp.Common.Dtos;
    using System;
    using System.Collections.Generic;
    public interface IWeatherApi
    {
        IList<HourlyForecast> Get12HTromsWeather();
        IList<HourlyForecast> Get24HTromsWeather();
        ForecastDto GetForecast(DateTime dateTime);
    }
}