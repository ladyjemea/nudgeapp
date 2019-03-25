﻿namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;

    public interface IEnvironmelntalInfoRepository : IRepository<WeatherForecastEntity>
    {
        Guid CreateInfo(ForecastDto info);
        ForecastDto GetForecast(Guid id);
    }
}