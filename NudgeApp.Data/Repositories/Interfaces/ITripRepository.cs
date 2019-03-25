namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;

    public interface ITripRepository: IRepository<TripEntity>
    {
        Guid Create(TripDto trip, Guid userId, Guid forecastId);
    }
}