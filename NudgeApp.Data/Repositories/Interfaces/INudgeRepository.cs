namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;

    public interface INudgeRepository : IRepository<NudgeEntity>
    {
        Guid Insert(TransportationType transportationType, Guid userId, Guid tripId);
    }
}