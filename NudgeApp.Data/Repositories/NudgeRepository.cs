namespace NudgeApp.Data.Repositories
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class NudgeRepository : Repository<NudgeEntity>, INudgeRepository
    {
        public NudgeRepository(INudgeDbContext context) : base(context) { }

        public Guid Create(NudgeDto nudge, Guid userId, Guid envInfoId)
        {
            var entity = new NudgeEntity
            {
                EnvironmentalInfoId = envInfoId,
                UserId = userId,
                NudgeResult = nudge.NudgeResult,
                TransportationType = nudge.TransportationType
            };

            this.Insert(entity);
            return entity.Id;
        }
    }
}
