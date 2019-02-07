namespace NudgeApp.DataManagement.ExternalApi.Weather.Interfaces
{
    using System.Collections.Generic;
    public interface IWeatherApi
    {
        IList<HourlyForecast> Get12HTromsWeather();
        IList<HourlyForecast> Get24HTromsWeather();
    }
}