namespace NudgeApp.DataManagement.ExternalApi.Weather.Interfaces
{
    using NudgeApp.Common.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWeatherApi
    {
        IList<HourlyForecast> Get12HTromsWeather();
        IList<HourlyForecast> Get24HTromsWeather();
        ForecastDto GetForecast(DateTime dateTime);
        Task<ForecastDto> GetCurrentForecast();
    }
}