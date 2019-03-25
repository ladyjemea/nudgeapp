namespace NudgeApp.DataManagement.Implementation
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public class NudgeLogic : INudgeLogic
    {
        private readonly INudgeRepository NudgeRepository;
        private readonly IEnvironmelntalInfoRepository EnvironmelntalInfoRepository;
        private readonly IUserRepository UserRepository;
        private readonly IPreferencesRepository PreferencesRepository;
        private readonly ITripRepository TripRepository;
        private readonly IAnonymousNudgeOracleRepository AnonymousNudgeOracleRepository;
        private readonly IAnonymousNudgeRepository AnonymousNudgeRepository;
        private readonly IWeatherService WeatherService;

        public NudgeLogic(INudgeRepository nudgeRepository, IEnvironmelntalInfoRepository environmelntalInfoRepository, IUserRepository userRepository, IPreferencesRepository preferencesRepository,
            ITripRepository tripRepository, IAnonymousNudgeOracleRepository anonymousNudgesRepository, IAnonymousNudgeRepository anonymousNudgeRepository, IWeatherService weatherApi)
        {
            this.NudgeRepository = nudgeRepository;
            this.EnvironmelntalInfoRepository = environmelntalInfoRepository;
            this.UserRepository = userRepository;
            this.PreferencesRepository = preferencesRepository;
            this.TripRepository = tripRepository;
            this.AnonymousNudgeOracleRepository = anonymousNudgesRepository;
            this.AnonymousNudgeRepository = anonymousNudgeRepository;
            this.WeatherService = weatherApi;
        }

        public void ManualNudge(NudgeDto nudge)
        {
            var forecast = this.WeatherService.Get24HTromsWeather().FirstOrDefault(f => f.DateTime.Hour == nudge.DepartureTime.Hour);

            var nudgeEntity = new AnonymousNudgeEntity
            {
                ActualTransportationType = nudge.TransportationType,
                PrecipitationProbability = forecast.PrecipitationProbability,
                Result = NudgeResult.Successful,
                RoadCondition = GetRoadCondition(forecast),
                SkyCoverage = GetSkyCoverage(forecast),
                Temperature = forecast.RealFeelTemperature.Value,
                Wind = forecast.Wind.Speed.Value
            };

            this.AnonymousNudgeOracleRepository.Insert(nudgeEntity);
        }

        public void AddNudge(Guid userId, TransportationType transportationType, ForecastDto forecast, TripDto trip)
        {
            var envId = this.EnvironmelntalInfoRepository.CreateInfo(forecast);
            this.NudgeRepository.Create(transportationType, userId, envId);
            this.TripRepository.Create(trip, userId, envId);

            try
            {
                this.AnonymousNudgeOracleRepository.Insert(new AnonymousNudgeEntity
                {
                    ActualTransportationType = transportationType,
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

        private RoadCondition GetRoadCondition(HourlyForecast forecast)
        {
            RoadCondition roadCondition;

            if (forecast.Temperature.Value <= 3)
            {
                if (forecast.SnowProbability < 20)
                {
                    roadCondition = RoadCondition.Ice;
                }
                else
                {
                    roadCondition = RoadCondition.Snow;
                }
            }
            else
            {
                if (forecast.RainProbability > 20)
                {
                    roadCondition = RoadCondition.Wet;
                }
                else
                {
                    roadCondition = RoadCondition.Dry;
                }
            }

            return roadCondition;
        }

        private SkyCoverageType GetSkyCoverage(HourlyForecast forecast)
        {
            SkyCoverageType coverage = forecast.CloudCover <= 15 ? SkyCoverageType.Clear : SkyCoverageType.PartlyCloudy;

            if (forecast.CloudCover > 75) coverage = SkyCoverageType.Cloudy;

            return coverage;
        }
    }
}
