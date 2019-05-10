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
        private readonly IAnonymousNudgeRepository AnonymousNudgeRepository;

        public NudgeService(INudgeRepository nudgeRepository, IPreferencesRepository preferencesRepository,
            INudgeOracleRepository anonymousNudgesRepository, IAnonymousNudgeRepository anonymousNudgeRepository)
        {
            this.NudgeRepository = nudgeRepository;
            this.PreferencesRepository = preferencesRepository;
            this.AnonymousNudgeOracleRepository = anonymousNudgesRepository;
            this.AnonymousNudgeRepository = anonymousNudgeRepository;
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
                    Wind = forecast.Wind,
                    UserPreferedTransportationType = this.PreferencesRepository.GetPreferences(userId).ActualTransportationType
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
                NudgeResult = NudgeResult.Unknown,
                UserId = userId,
                Type = TripType.Walk,
                SkyCoverage = forecast.SkyCoverage,
                Probability = forecast.Probabilities,
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
                    UserPreferedTransportationType = this.PreferencesRepository.GetPreferences(userId).ActualTransportationType
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while inserting into oracle database.");
                Console.WriteLine(ex.Message);
            }

            return id;
        }

        public void Test()
        {
            var random = new Random();

            for (int j = 0; j < 1000; j++)
            {
                for (int i = 0; i < 1000; i++)
                {
                    var entity = new OracleNudgeEntity()
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

                var entity2 = new OracleNudgeEntity()
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
