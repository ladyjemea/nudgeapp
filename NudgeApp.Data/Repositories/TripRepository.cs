namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class TripRepository : ITripRepository
    {
        private readonly INudgeDbContext Db;
        public TripRepository(INudgeDbContext db)
        {
            this.Db = db;
        }

        public Guid Create(TripDto trip, Guid userId, Guid envInfoId)
        {
            var entity = new TripEntity
            {
                EnvironmentalInfoId = envInfoId,
                UserId = userId,
                DistanceTraveled = trip.Distance,
                UsedTransportationType = trip.TransportationType
            };

            var result = this.Db.TripEntity.Add(entity);
            return result.Entity.Id;
        }

        public TripDto Get(Guid id)
        {
            var entity = this.Db.GetAll<TripEntity>().Where(e => e.Id == id).FirstOrDefault();

            TripDto result = null;
            if (entity != null)
            {
                result = new TripDto
                {
                    Distance = entity.DistanceTraveled,
                    TransportationType = entity.UsedTransportationType
                };
            }

            return result;
        }
    }
}
