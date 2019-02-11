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
        private readonly IWeatherApi WeatherApi;
        public Analyzer(IWeatherApi weather)
        {
            this.WeatherApi = weather;
        }

        public IList<DateInfo> AnalyseWeather()
        {
            var results = this.WeatherApi.Get12HTromsWeather();
            var neededResults = results.Select(r => new DateInfo
            {
                date = r.DateTime,
                temp = r.Temperature,
                realfeel = r.RealFeelTemperature,
                ceiling = r.Ceiling,
                visibility = r.Visibility,
                rain = r.Rain,
                rainprobability = r.RainProbability,
                snow = r.Snow,
                snowprobability = r.SnowProbability,
                ice = r.Ice,
                iceprobability = r.IceProbability,
                wind = r.Wind,
                daylight = r.IsDaylight
            }).ToList();
            /* var temperature = results.Select(r => new DateInfo { date = r.DateTime, temp = r.Temperature}).ToList();
             var ceiling = results.Select(r => new DateInfo { date = r.DateTime, ceiling = r.Ceiling }).ToList();
             var realfeel = results.Select(r => new DateInfo { date = r.DateTime, realfeel = r.RealFeelTemperature }).ToList();
             var rain = results.Select(r => new DateInfo { date = r.DateTime, rain = r.Rain }).ToList();
             var visibility = results.Select(r => new DateInfo { date = r.DateTime, visibility = r.Visibility }).ToList();
             var snow = results.Select(r => new DateInfo { date = r.DateTime, snow = r.Snow }).ToList();
             var ice = results.Select(r => new DateInfo { date = r.DateTime, ice = r.Ice }).ToList();
             var rainprobsbility = results.Select(r => new DateInfo { date = r.DateTime, rainprobability = r.RainProbability }).ToList();
             var snowprobability = results.Select(r => new DateInfo { date = r.DateTime, snowprobability = r.SnowProbability }).ToList();
             var iceprobability = results.Select(r => new DateInfo { date = r.DateTime, iceprobability = r.IceProbability }).ToList();
             var wind = results.Select(r => new DateInfo { date = r.DateTime, wind = r.Wind }).ToList();
             var daylight = results.Select(r => new DateInfo { date = r.DateTime, daylight = r.IsDaylight }).ToList(); */

            //var reallyColdTemperature = results.Where(r => r.Temperature.Value < -2).Select(r => new DateInfo { date = r.DateTime, temp = r.Temperature }).ToList();
            //var outdoorTemperature = results.Where(r => r.Temperature.Value < 10 && r.IsDaylight == false).Select(r => new DateInfo { date = r.DateTime, temp = r.Temperature, daylight = r.IsDaylight}).ToList();
            var eventStartTime = new DateTime(2019, 2, 7, 20, 0, 0);
            var outdoorTemp = results.Where(r => r.DateTime > eventStartTime.AddHours(-1) && r.DateTime < eventStartTime).Select(r => new DateInfo { date = r.DateTime, temp = r.Temperature, daylight = r.IsDaylight }).First();
            if (outdoorTemp.temp.Value > 20)
            {
                // save to the db the EnvironmananfkafInfo
                // go walking/ send the nudge + an id to the env info
            }
            else if (outdoorTemp.temp.Value > 10)
            {
                // take the bus
            }

            return neededResults;
            //return outdoorTemperature;
        }

        public class DateInfo
        {
            public DateTime date;
            public WeatherInfo temp;
            public WeatherInfo ceiling;
            public WeatherInfo realfeel;
            public WeatherInfo rain;
            public WeatherInfo visibility;
            public WeatherInfo snow;
            public WeatherInfo ice;
            public int rainprobability;
            public int snowprobability;
            public int iceprobability;
            public WindInfo wind;
            public bool daylight;

        }
    }
}
