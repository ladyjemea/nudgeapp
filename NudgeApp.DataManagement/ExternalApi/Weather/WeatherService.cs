namespace NudgeApp.DataManagement.ExternalApi.Weather
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Newtonsoft.Json;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using RestSharp;

    public class WeatherService : IWeatherService
    {
        private const string APIkey = "XysYFpuGxOXOJNf6zXLk6fAy7pnH1yCr";
        private const string Locationkey = "256116"; // Tromsø 
        private const string serviceString = "http://dataservice.accuweather.com";



        public IList<HourlyForecast> Get12HTromsWeather()
        {
            var client = new RestClient(serviceString);
            var request = new RestRequest("forecasts/v1/hourly/12hour/" + Locationkey, Method.GET);
            request.AddParameter("apikey", APIkey);
            request.AddParameter("language", "en-us");
            request.AddParameter("details", true);
            request.AddParameter("metric", true);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<HourlyForecast>>(response.Content);
        }
        public IList<HourlyForecast> Get24HTromsWeather()
        {
            throw new Exception("Not included in free package.");
            /*
            var client = new RestClient(serviceString);
            var request = new RestRequest("forecasts/v1/hourly/24hour/" + Locationkey, Method.GET);
            request.AddParameter("apikey", APIkey);
            request.AddParameter("language", "en-us");
            request.AddParameter("details", true);
            request.AddParameter("metric", true);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<HourlyForecast>>(response.Content);*/
        }
        public IList<DateInfo> AnalyseWeather()
        {
            var results = this.Get12HTromsWeather();
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

            return neededResults;
            //return outdoorTemperature;
        }

        public class DateInfo
        {
            public DateTime date;
            public UnitInfo temp;
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
            public int PrecipitationProbability;

        }

        public WeatherDto GetForecast(DateTime dateTime)
        {
            if (DateTime.Now.AddHours(12) < dateTime)
                throw new Exception("Date too far in the future. Forecast unreliable.");

            var longForecast = this.Get12HTromsWeather();
            var forecast = longForecast.First(f => f.DateTime.Hour == dateTime.Hour);

            return new WeatherDto
            {
                RawData = new WeatherRawData
                {
                    Date = dateTime,
                    Time = dateTime,
                    Temperature = forecast.Temperature.Value,
                    RealFeelTemperature = forecast.RealFeelTemperature.Value,
                    Ceiling = forecast.Ceiling.Value,
                    Rain = forecast.Rain.Value,
                    RainProbability = forecast.RainProbability,
                    Snow = forecast.Snow.Value,
                    SnowProbability = forecast.SnowProbability,
                    Ice = forecast.Ice.Value,
                    IceProbability = forecast.IceProbability,
                    Visibility = forecast.Visibility.Value,
                    Wind = forecast.Wind.Speed.Value,
                    Daylight = forecast.IsDaylight
                },
                RoadCondition = GetRoadCondition(forecast),
                SkyCoverage = GetSkyCoverage(forecast.CloudCover),

            };
        }

        public async Task<WeatherDto> GetCurrentForecast()
        {
            var forecastList = await this.GetCurrentTromsForecast();
            var forecast = forecastList.First();

            return new WeatherDto
            {
                RawData = new WeatherRawData
                {
                    Date = forecast.LocalObservationDateTime,
                    Time = forecast.LocalObservationDateTime,
                    Temperature = forecast.Temperature.Metric.Value,
                    RealFeelTemperature = forecast.RealFeelTemperature.Metric.Value,
                    Ceiling = forecast.Ceiling.Metric.Value,
                    Wind = forecast.Wind.Speed.Value,
                    PrecipitationProbability = forecast.HasPrecipitation ? 100 : 0,
                },
                RoadCondition = forecast.HasPrecipitation ? RoadCondition.Wet : RoadCondition.Dry
            };
        }

        private Task<List<CurrentForecast>> GetCurrentTromsForecast()
        {
            var client = new RestClient(serviceString);
            var request = new RestRequest("currentconditions/v1/" + Locationkey, Method.GET);
            request.AddParameter("apikey", APIkey);
            request.AddParameter("language", "en-us");
            request.AddParameter("details", true);
            request.AddParameter("metric", true);
            request.OnBeforeDeserialization = resp => { resp.ContentType = "application/json"; resp.ContentEncoding = "UTF-8"; };

            var taskCompletionSource = new TaskCompletionSource<List<CurrentForecast>>();
            client.ExecuteAsync<List<CurrentForecast>>(request, response => taskCompletionSource.SetResult(response.Data));

            return taskCompletionSource.Task;
        }

        private SkyCoverageType GetSkyCoverage(int cloudCoverPercentage)
        {
            SkyCoverageType coverage = cloudCoverPercentage <= 15 ? SkyCoverageType.Clear : SkyCoverageType.PartlyCloudy;

            if (cloudCoverPercentage > 75) coverage = SkyCoverageType.Cloudy;

            return coverage;
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

       
    }
}
