namespace NudgeApp.Data.Repositories
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class NudgeRepository : Repository<NudgeEntity>, INudgeRepository
    {
        public NudgeRepository(INudgeDbContext context) : base(context) { }

        public Guid Insert(TransportationType transportationType, Guid userId, Guid tripId)
        {
            var entity = new NudgeEntity
            {
                TripId = tripId,
                UserId = userId,
                NudgeResult = NudgeResult.Successful,
                TransportationType = transportationType
            };

            this.Insert(entity);
            return entity.Id;
        }
    }
}
