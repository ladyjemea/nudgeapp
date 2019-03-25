﻿namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;

    public interface IEnvironmelntalInfoRepository : IRepository<WeatherForecastEntity>
    {
        Guid CreateInfo(WeatherDto info);
        WeatherDto GetForecast(Guid id);
    }
}