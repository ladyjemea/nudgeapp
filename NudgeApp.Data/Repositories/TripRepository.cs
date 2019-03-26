namespace NudgeApp.Data.Repositories
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class TripRepository : Repository<TripEntity>, ITripRepository
    {
        public TripRepository(INudgeDbContext context) : base(context) { }

        public Guid Insert(TripDto trip, Guid userId, Guid forecastId)
        {
            var entity = new TripEntity
            {
                WeatherForecastId = forecastId,
                DistanceTraveled = trip.Distance,
                Type = TripType.WithDestinaion,
                UsedTransportationType = trip.TransportationType
            };

            this.Insert(entity);
            return entity.Id;
        }

        public Guid Insert (Guid userId, Guid forecastId)
        {
            var entity = new TripEntity
            {
                WeatherForecastId = forecastId,
                Type = TripType.Walk
            };

            this.Insert(entity);
            return entity.Id;
        }
    }
}
