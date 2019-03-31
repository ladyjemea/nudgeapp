namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;

    public interface ITripRepository: IRepository<TripEntity>
    {
        Guid Insert(TripDto trip, Guid userId, Guid forecastId);
        Guid Insert(Guid userId, Guid forecastId);
    }
}