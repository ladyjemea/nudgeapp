namespace NudgeApp.Data.Repositories
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class TripRepository : Repository<TripEntity>, ITripRepository
    {
        public TripRepository(INudgeDbContext context) : base(context) { }

        public Guid Create(TripDto trip, Guid userId, Guid forecastId)
        {
            var entity = new TripEntity
            {
                WeatherForecastId = forecastId,
                UserId = userId,
                DistanceTraveled = trip.Distance,
                UsedTransportationType = trip.TransportationType
            };

            this.Insert(entity);
            return entity.Id;
        }
    }
}
