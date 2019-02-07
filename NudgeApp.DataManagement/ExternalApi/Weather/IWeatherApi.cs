namespace NudgeApp.DataManagement.ExternalApi.Weather
{
    using System.Collections.Generic;
    public interface IWeatherApi
    {
        IList<HourlyForecast> Get12HTromsWeather();
        IList<HourlyForecast> Get24HTromsWeather();
    }
}