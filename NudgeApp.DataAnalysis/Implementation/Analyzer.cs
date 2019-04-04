using NudgeApp.Common.Dtos;
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
        public Analyzer(IWeatherService weather)
        {
            this.WeatherService = weather;
        }

        public WeatherDto AnalyseWeather()
        {
            //var results = this.WeatherService.Get12HTromsWeather();
            return this.WeatherService.GetForecast(DateTime.Now.AddHours(1));
            
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


