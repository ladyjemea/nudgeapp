namespace NudgeApp.DataManagement.ExternalApi.Weather.Interfaces
{
    using NudgeApp.Common.Dtos;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IWeatherService
    {
        IList<HourlyForecast> Get12HTromsWeather();
        WeatherDto GetForecast(DateTime dateTime);
        Task<WeatherDto> GetCurrentForecast();
        Task<WeatherDto> GetCurrentForecastRandom();
    }
}