namespace NudgeApp.Data.Repositories
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class NudgeRepository : Repository<NudgeEntity>, INudgeRepository
    {
        public NudgeRepository(INudgeDbContext context) : base(context) { }

        public Guid Create(TransportationType transportationType, Guid userId, Guid forecastId)
        {
            var entity = new NudgeEntity
            {
                WeatherForecastId = forecastId,
                UserId = userId,
                NudgeResult = NudgeResult.Successful,
                TransportationType = transportationType
            };

            this.Insert(entity);
            return entity.Id;
        }
    }
}
