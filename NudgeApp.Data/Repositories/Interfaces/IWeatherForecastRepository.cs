namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;

    public interface IWeatherForecastRepository : IRepository<WeatherForecastEntity>
    {
        Guid Insert(WeatherDto info);
        WeatherDto GetForecast(Guid id);
    }
}