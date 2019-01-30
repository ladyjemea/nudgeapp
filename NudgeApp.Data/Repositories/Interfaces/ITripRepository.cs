namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    public interface ITripRepository
    {
        Guid Create(TripDto trip, Guid userId, Guid envInfoId);
        TripDto Get(Guid id);
    }
}