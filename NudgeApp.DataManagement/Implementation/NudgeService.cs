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
        private readonly IWeatherForecastRepository WeatherForecastRepository;
        private readonly IPreferencesRepository PreferencesRepository;
        private readonly ITripRepository TripRepository;
        private readonly IAnonymousNudgeOracleRepository AnonymousNudgeOracleRepository;
        private readonly IAnonymousNudgeRepository AnonymousNudgeRepository;

        public NudgeService(INudgeRepository nudgeRepository, IWeatherForecastRepository weatherForecastRepository, IPreferencesRepository preferencesRepository,
            ITripRepository tripRepository, IAnonymousNudgeOracleRepository anonymousNudgesRepository, IAnonymousNudgeRepository anonymousNudgeRepository)
        {
            this.NudgeRepository = nudgeRepository;
            this.WeatherForecastRepository = weatherForecastRepository;
            this.PreferencesRepository = preferencesRepository;
            this.TripRepository = tripRepository;
            this.AnonymousNudgeOracleRepository = anonymousNudgesRepository;
            this.AnonymousNudgeRepository = anonymousNudgeRepository;
        }

        public void AddNudge(Guid userId, TransportationType transportationType, WeatherDto forecast, TripDto trip)
        {
            var forecastId = this.WeatherForecastRepository.Insert(forecast);

            Guid tripId;
            if (trip == null)
                tripId = this.TripRepository.Insert(userId, forecastId);
            else
                tripId = this.TripRepository.Insert(trip, userId, forecastId);

            this.NudgeRepository.Insert(transportationType, userId, tripId);

            if (nudgeData.Trip != null)
                this.TripRepository.Create(nudgeData.Trip, userId, weatherForecastId);

            this.NudgeRepository.Create(nudgeData.TransportationType, userId, weatherForecastId);

            try
            {
                var anonymousNudge = new AnonymousNudgeEntity
                {
                    ActualTransportationType = transportationType,
                    PrecipitationProbability = forecast.RawData.PrecipitationProbability,
                    Result = NudgeResult.Successful,
                    RoadCondition = forecast.RoadCondition,
                    SkyCoverage = forecast.SkyCoverage,
                    Temperature = forecast.RawData.Temperature,
                    Wind = forecast.RawData.Wind,
                    UserPreferedTransportationType = this.PreferencesRepository.GetPreferences(userId).ActualTransportationType
                };

                if (nudgeData.Forecast != null)
                {
                    anonymousNudge.PrecipitationProbability = nudgeData.Forecast.PrecipitationProbability;
                    anonymousNudge.RoadCondition = nudgeData.Forecast.RoadCondition;
                    anonymousNudge.SkyCoverage = nudgeData.Forecast.SkyCoverage;
                    anonymousNudge.Temperature = nudgeData.Forecast.Temperature;
                    anonymousNudge.Wind = nudgeData.Forecast.Wind;
                }
                this.AnonymousNudgeOracleRepository.Insert(anonymousNudge);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while inserting into oracle database.");
                Console.WriteLine(ex.Message);
            }
        }

        public void Test()
        {
            var random = new Random();

            for (int j = 0; j < 1000; j++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var entity = new AnonymousNudgeEntity()
                    {
                        ActualTransportationType = (TransportationType)(random.Next() % 3),
                        PrecipitationProbability = random.Next() % 100,
                        Result = (NudgeResult)(random.Next() % 3),
                        RoadCondition = (RoadCondition)(random.Next() % 3),
                        SkyCoverage = (SkyCoverageType)(random.Next() % 3),
                        Temperature = random.Next() % 100 - 50,
                        UserPreferedTransportationType = (TransportationType)(random.Next() % 5),
                        Wind = random.Next() % 100
                    };

                    this.AnonymousNudgeRepository.InsertWIthNoSave(entity);
                }

                var entity2 = new AnonymousNudgeEntity()
                {
                    ActualTransportationType = (TransportationType)(random.Next() % 3),
                    PrecipitationProbability = random.Next() % 100,
                    Result = (NudgeResult)(random.Next() % 3),
                    RoadCondition = (RoadCondition)(random.Next() % 3),
                    SkyCoverage = (SkyCoverageType)(random.Next() % 3),
                    Temperature = random.Next() % 100 - 50,
                    UserPreferedTransportationType = (TransportationType)(random.Next() % 5),
                    Wind = random.Next() % 100
                };

                this.AnonymousNudgeRepository.Insert(entity2);
            }
            // this.AnonymousNudgesRepository.SelectAll();
        }
    }
}
