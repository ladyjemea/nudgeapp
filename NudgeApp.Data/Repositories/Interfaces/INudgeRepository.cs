namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;

    public interface INudgeRepository : IRepository<NudgeEntity>
    {
        Guid Create(NudgeDto nudge, Guid userId, Guid envInfoId);
    }
}