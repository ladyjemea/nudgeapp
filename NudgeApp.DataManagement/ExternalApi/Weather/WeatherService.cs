namespace NudgeApp.DataManagement.ExternalApi.Weather
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
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

        private readonly IDistributedCache DistributedCache;

        public WeatherService(IDistributedCache distributedCache)
        {
            this.DistributedCache = distributedCache;
        }

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
                    Precipitation = forecast.PrecipitationProbability,
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
                PrecipitationCondition = GetPrecipitation(forecast),
                WeatherCondition = GetWeatherCondition(forecast)
                //Probabilities = GetProbabilities(forecast),

            };
        }

        public async Task<WeatherDto> GetCurrentForecast()
        {
            CurrentForecast forecast = null;

            // forecast = await this.GetCurrentForecastFromRedis();

            if (forecast == null)
            {
                // var forecastList = await this.GetCurrentTromsForecast();
                // forecast = forecastList.First();

                forecast = new CurrentForecast
                {
                    CloudCover = 3
                };

               // this.SaveCurrentForecastToRedis(forecast);
            }

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

        private async void SaveCurrentForecastToRedis(CurrentForecast forecast)
        {
            var key = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0).ToString();
            var token = new CancellationToken();

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, forecast);
                await this.DistributedCache.SetAsync(key, ms.ToArray(), token);
            }

        }

        private async Task<CurrentForecast> GetCurrentForecastFromRedis()
        {
            var token = new CancellationToken();
            var key = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0).ToString();
            var data = await this.DistributedCache.GetAsync(key, token);

            if (data == null)
                return null;

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (CurrentForecast)obj;
            }
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

        private PrecipitationCondition GetPrecipitation(HourlyForecast forecast)
        {
            PrecipitationCondition precipitation;

            if (forecast.PreciptationProbability > 40)
            {
                if (forecast.RainProbability > forecast.SnowProbability)
                {
                    precipitation = PrecipitationCondition.Rainy;
                }
                else
                {
                    precipitation = PrecipitationCondition.Snowy;
                }

            }
            else
            {
                precipitation = PrecipitationCondition.NoPrecipitation;
            }
            return precipitation;
        }

        private WeatherCondition GetWeatherCondition(HourlyForecast forecast)
        {
            WeatherCondition windy;

            if (forecast.WindGust.Speed.Value > 15 && forecast.Wind.Speed.Value > 12)
            {
                windy = WeatherCondition.WindyWithGust;
            }
            else
            {
                if (forecast.WindGust.Speed.Value < 5 && forecast.Wind.Speed.Value > 12)
                {
                    windy = WeatherCondition.Windy;
                }
                else
                {
                    windy = WeatherCondition.Gust;
                }
            }

            if (forecast.WindGust.Speed.Value > 15 && forecast.Wind.Speed.Value < 5)
            {
                windy = WeatherCondition.GustWithNoWind;
            }
            else
            {
                if (forecast.WindGust.Speed.Value < 5 && forecast.Wind.Speed.Value < 5)
                {
                    windy = WeatherCondition.Calm;
                }
                else
                {
                    windy = WeatherCondition.CalmWinds;

                }
            }
            return windy;
        }


        private Probabilities GetProbabilities(HourlyForecast forecast)
        {
            Probabilities probabilities ;

            if (forecast.Rain.Value > 20)
            {
                probabilities = Probabilities.Rain;
            }
            if (forecast.Snow.Value > 20)
            {
                probabilities = Probabilities.Snow;
            }
            if (forecast.Ice.Value > 20)
            {
                probabilities = Probabilities.Ice;
            }
            if (forecast.Snow.Value > 20 && 10 < forecast.Rain.Value && forecast.Rain.Value < 20)
            {
                probabilities = Probabilities.Slippery;
            }
            else
            {
                probabilities = Probabilities.Normal;
            }
            return probabilities;
        }


       

        //private WeatherCondition GetWeatherCondition(CurrentForecast forecast)
        //{
        //    WeatherCondition weatherCondition;

        //    if (forecast.Rain)
        //}

    }
}
