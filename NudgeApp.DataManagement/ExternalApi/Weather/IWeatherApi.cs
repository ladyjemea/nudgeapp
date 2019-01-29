namespace NudgeApp.DataManagement.ExternalApi.Weather
{
    using System.Collections.Generic;
    public interface IWeatherApi
    {
        IList<HourlyForecast> GetTromsWeather();
    }
}