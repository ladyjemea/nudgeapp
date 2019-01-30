namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class EnvironmelntalInfoRepository : IEnvironmelntalInfoRepository
    {
        private readonly INudgeDbContext Db;
        public EnvironmelntalInfoRepository(NudgeDbContext db)
        {
            this.Db = db;
        }

        public Guid Create(EnvironmelntalInfoDto info)
        {
            var entity = new EnvironmentalInfoEntity
            {
                CloudCoveragePercent = info.CloudCoveragePercent,
                RoadCondition = info.RoadCondition,
                Temperature = info.Temperature,
                Time = info.Time,
                Wind = info.Wind
            };

            var result = this.Db.EnvironmentalInfoEntity.Add(entity);
            return result.Entity.Id;
        }

        public EnvironmelntalInfoDto Get(Guid id)
        {
            var entity = this.Db.GetAll<EnvironmentalInfoEntity>().Where(e => e.Id == id).FirstOrDefault();

            EnvironmelntalInfoDto result = null;
            if (entity != null)
            {
                result = new EnvironmelntalInfoDto
                {
                    CloudCoveragePercent = entity.CloudCoveragePercent,
                    RoadCondition = entity.RoadCondition,
                    Temperature = entity.Temperature,
                    Time = entity.Time,
                    Wind = entity.Wind
                };
            }

            return result;
        }
    }
}
