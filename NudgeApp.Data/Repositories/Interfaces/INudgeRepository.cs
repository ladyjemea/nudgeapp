namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Common.Dtos;
    public interface INudgeRepository
    {
        Guid Create(NudgeDto nudge, Guid userId, Guid envInfoId);
        NudgeDto Get(Guid id);
    }
}