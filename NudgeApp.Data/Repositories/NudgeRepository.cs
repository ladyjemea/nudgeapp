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

        public Guid Insert(NudgeResult result, Guid userId, WeatherDto forecast, TripDto trip)
        {
            var entity = new NudgeEntity
            {
                UserId = userId,
                NudgeResult = result,
                TransportationType = trip.TransportationType,
                Distance = trip.Distance,
                Duration = trip.Duration.Minutes,
                SkyCoverage = forecast.SkyCoverage,
                Probability = forecast.Probabilities,
                ReafFeelTemperature = forecast.RawData.RealFeelTemperature,
                Temperature = forecast.RawData.Temperature,
                RoadCondition = forecast.RoadCondition,
                Time = forecast.RawData.Time,
                Wind = forecast.Windy
            };

            this.Insert(entity);
            return entity.Id;
        }
    }
}
