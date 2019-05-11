using NudgeApp.Common.Dtos;
using NudgeApp.Data.OracleDb.Queries;
using NudgeApp.DataManagement.ExternalApi.Weather;
using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NudgeApp.DataAnalysis.API
{
    public class Analyzer : IAnalyzer
    {
        private readonly IWeatherService WeatherService;
        private readonly INudgeOracleRepository NudgeOracleRepository;

        private Random random;

        public Analyzer(IWeatherService weather, INudgeOracleRepository nudgeOracleRepository)
        {
            this.WeatherService = weather;
            this.NudgeOracleRepository = nudgeOracleRepository;

            this.random = new Random();
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


