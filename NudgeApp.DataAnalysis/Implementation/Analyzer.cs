namespace NudgeApp.DataAnalysis.API
{
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.DataManagement.ExternalApi.Travel;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Helpers;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class Analyzer : IAnalyzer
    {
        private readonly IWeatherService WeatherService;
        private readonly INudgeOracleRepository NudgeOracleRepository;
        private readonly IWalkService WalkService;
        private readonly IPushNotificationService PushNotificationService;

        private Random random;

        public Analyzer(IWeatherService weather, INudgeOracleRepository nudgeOracleRepository, IWalkService walkService, IPushNotificationService pushNotificationService)
        {
            this.WeatherService = weather;
            this.NudgeOracleRepository = nudgeOracleRepository;
            this.WalkService = walkService;
            this.PushNotificationService = pushNotificationService;

            this.random = new Random();
        }



        public async Task AnalyseEvent(Guid userId, UserEvent userEvent, Coordinates userLocation)
        {
            if (DateTime.Now.AddHours(-1) <= userEvent.Start &&  userEvent.Start <= DateTime.Now)
            {
                var result = await this.WalkService.WalkInfo(userLocation, userEvent.Location);

                var duration = result.rows.FirstOrDefault().elements.FirstOrDefault().duration;

                this.PushNotificationService.PushToUser(userId, "You have a meeting!", $"If you leave now walking, you'll make {duration} minutes to your event. Check Google maps");
                
            }
        }


        public WeatherDto AnalyseWeather()
        {
            //var results = this.WeatherService.Get12HTromsWeather();
            return this.WeatherService.GetForecast(DateTime.Now.AddHours(1));

        }

        public bool ShouldINudge(DayOfWeek dayOfWeek)
        {
            var (succesfulCount, _) = this.NudgeOracleRepository.ApproxCount(new QueryFilter
            {
                Result = Common.Enums.NudgeResult.Successful,
                MinTemperature = 15,
                WeekDay = dayOfWeek,
                MaxPrecipitation = 50
            });

            var (failCount, _) = this.NudgeOracleRepository.ApproxCount(new QueryFilter
            {
                Result = Common.Enums.NudgeResult.Failed,
                MinTemperature = 15,
                WeekDay = dayOfWeek,
                MaxPrecipitation = 50
            });

            var succesfulPercentage = succesfulCount * 100 / (succesfulCount + failCount);

            if (succesfulPercentage > 80)
            {
                return true;
            }
            else if ((20 < succesfulPercentage) && (succesfulPercentage < 80))
            {
                var value = this.random.Next(100);

                if (value <= succesfulPercentage)
                    return true;
            }
            else
            {
                return false;
            }

            return false;
        }

        public class DateInfo
        {
            public DateTime date;
            public float temp;
            public UnitInfo ceiling;
            public UnitInfo realfeel;
            public UnitInfo rain;
            public UnitInfo visibility;
            public UnitInfo snow;
            public UnitInfo ice;
            public int rainprobability;
            public int snowprobability;
            public int iceprobability;
            public WindInfo wind;
            public bool daylight;
            internal WindInfo windgust;
        }
    }
}


