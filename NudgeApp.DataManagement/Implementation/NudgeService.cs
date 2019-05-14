namespace NudgeApp.DataManagement.Implementation
{
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;

    public class NudgeService : INudgeService
    {
        private readonly INudgeRepository NudgeRepository;
        private readonly IPreferencesRepository PreferencesRepository;
        private readonly INudgeOracleRepository AnonymousNudgeOracleRepository;

        public NudgeService(INudgeRepository nudgeRepository, IPreferencesRepository preferencesRepository, INudgeOracleRepository anonymousNudgesRepository)
        {
            this.NudgeRepository = nudgeRepository;
            this.PreferencesRepository = preferencesRepository;
            this.AnonymousNudgeOracleRepository = anonymousNudgesRepository;
        }

        public void AddNudge(Guid userId, NudgeResult nudgeResult, WeatherDto forecast, TripDto trip)
        {
            this.NudgeRepository.Insert(nudgeResult, userId, forecast, trip);

            try
            {
                this.AnonymousNudgeOracleRepository.Insert(new OracleNudgeEntity
                {
                    ActualTransportationType = trip.TransportationType,
                    PrecipitationProbability = forecast.PrecipitationProbability,
                    Result = NudgeResult.Successful,
                    RoadCondition = forecast.RoadCondition,
                    SkyCoverage = forecast.SkyCoverage,
                    Temperature = forecast.Temperature,
                    Wind = forecast.Wind
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while inserting into oracle database.");
                Console.WriteLine(ex.Message);
            }
        }

        public Guid AddNudge(Guid userId, WeatherDto forecast)
        {
            var id = this.NudgeRepository.Insert(new NudgeEntity
            {
                Result = NudgeResult.Unknown,
                UserId = userId,
                Type = TripType.Walk,
                SkyCoverage = forecast.SkyCoverage,
                WeatherProbability = forecast.Probabilities,
                ReafFeelTemperature = forecast.RealFeelTemperature,
                Temperature = forecast.Temperature,
                RoadCondition = forecast.RoadCondition,
                DateTime = forecast.DateTime,
                WindCondition = forecast.WindCondition
            });

            try
            {

                this.AnonymousNudgeOracleRepository.Insert(new OracleNudgeEntity
                {
                    PrecipitationProbability = forecast.PrecipitationProbability,
                    Result = NudgeResult.Successful,
                    RoadCondition = forecast.RoadCondition,
                    SkyCoverage = forecast.SkyCoverage,
                    Temperature = forecast.Temperature,
                    Wind = forecast.Wind,
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while inserting into oracle database.");
                Console.WriteLine(ex.Message);
            }

            return id;
        }
    }
}
