namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;

    public interface INudgeRepository : IRepository<NudgeEntity>
    {
        Guid Insert(NudgeResult successful, Guid userId, WeatherDto forecast, TripDto trip);
    }
}