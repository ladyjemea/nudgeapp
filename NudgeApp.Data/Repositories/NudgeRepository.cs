namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class NudgeRepository : INudgeRepository
    {
        private readonly INudgeDbContext Db;
        public NudgeRepository(INudgeDbContext db)
        {
            this.Db = db;
        }

        public Guid Create(NudgeDto nudge, Guid userId, Guid envInfoId)
        {
            var entity = new NudgeEntity
            {
                EnvironmentalInfoId = envInfoId,
                UserId = userId,
                NudgeResult = nudge.NudgeResult,
                TransportationType = nudge.TransportationType
            };

            var result = this.Db.NudgeEntity.Add(entity);
            return result.Entity.Id;
        }

        public NudgeDto Get(Guid id)
        {
            var entity = this.Db.GetAll<NudgeEntity>().Where(e => e.Id == id).FirstOrDefault();

            NudgeDto result = null;
            if (entity != null)
            {
                result = new NudgeDto
                {
                    NudgeResult = entity.NudgeResult,
                    TransportationType = entity.TransportationType
                };
            }

            return result;
        }
    }
}
