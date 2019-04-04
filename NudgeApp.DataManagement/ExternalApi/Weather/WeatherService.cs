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
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using RestSharp;

    public class WeatherService : IWeatherService
    {
        private const string APIkey = "XysYFpuGxOXOJNf6zXLk6fAy7pnH1yCr";
        private const string Locationkey = "256116"; // Tromsø 
        private const string serviceString = "http://dataservice.accuweather.com";

        private readonly IMemoryCacheService MemoryCacheService;

        public WeatherService(IMemoryCacheService memoryCacheService)
        {
            this.MemoryCacheService = memoryCacheService;
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

            var weatherDto = new WeatherDto
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
                    WindGust = forecast.WindGust.Speed.Value,
                    Daylight = forecast.IsDaylight
                },
                RoadCondition = GetRoadCondition(forecast),
                SkyCoverage = GetSkyCoverage(forecast.CloudCover),
                PrecipitationCondition = GetPrecipitation(forecast),
                WeatherCondition = GetWeatherCondition(forecast),
                Others = GetOthers(forecast)
                // Probabilities = GetProbabilities(forecast),
            };

            return weatherDto;
        }

        public async Task<WeatherDto> GetCurrentForecast()
        {
            var key = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0).ToString();
            var weatherDto = await this.MemoryCacheService.GetAsync<WeatherDto>(key);

            if (weatherDto == null)
            {
                var forecastList = await this.GetCurrentTromsForecast();
                var forecast = forecastList.First();

                weatherDto = new WeatherDto
                {
                    RawData = new WeatherRawData()
                    {
                        Date = forecast.LocalObservationDateTime,
                        Time = forecast.LocalObservationDateTime,
                        Temperature = forecast.Temperature.Metric.Value,
                        RealFeelTemperature = forecast.RealFeelTemperature.Metric.Value,
                        Ceiling = forecast.Ceiling.Metric.Value,
                        Wind = forecast.Wind.Speed.Value,
                        PrecipitationProbability = forecast.HasPrecipitation ? 100 : 0,
                        CloudCoveragePercent = forecast.CloudCover
                    },
                    RoadCondition = forecast.HasPrecipitation ? RoadCondition.Wet : RoadCondition.Dry
                };

                await this.MemoryCacheService.SaveAsync(key, weatherDto);
            }

            return weatherDto;
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
            WeatherCondition weather;

            if (forecast.WindGust.Speed.Value >= 25 && forecast.Wind.Speed.Value >= 20)
            {
                weather = WeatherCondition.StrongWinds;
            }
            else
            {
                if (forecast.WindGust.Speed.Value >= 10 && 24 <= forecast.WindGust.Speed.Value
                    && forecast.Wind.Speed.Value >= 10 && 24 <= forecast.Wind.Speed.Value)
                {
                    weather = WeatherCondition.LightWinds;
                }
                else
                {
                    weather = WeatherCondition.Calm;
                }
            }

            if (forecast.Rain.Value >= 20)
            {
                weather = WeatherCondition.Rain;
            }
            else
            {
                weather = WeatherCondition.NoRain;
            }

            if (forecast.Snow.Value >= 20)
            {
                weather = WeatherCondition.Snow;
            }
            else
            {
                weather = WeatherCondition.NoSnow;
            }

            if (forecast.Temperature.Value < -10)
            {
                weather = WeatherCondition.Freezing;
            }
            else
            {
                if (forecast.Temperature.Value > -9 && 8 < forecast.Temperature.Value)
                {
                    weather = WeatherCondition.Cold;
                }
                if (forecast.Temperature.Value > 8 && 14 < forecast.Temperature.Value)
                {
                    weather = WeatherCondition.Cool;
                }
                else
                {
                    weather = WeatherCondition.Warm;
                }
            }
            return weather;
        }


        private Probabilities GetProbabilities(HourlyForecast forecast)
        {
            Probabilities probabilities;

            if (forecast.RainProbability> 40)
            {
                probabilities = Probabilities.Rain;
            }
            if (forecast.SnowProbability > 40)
            {
                probabilities = Probabilities.Snow;
            }
            if (forecast.IceProbability > 40)
            {
                probabilities = Probabilities.Ice;
            }
            if (forecast.RainProbability> 40 && 10 < forecast.RainProbability && forecast.Temperature.Value > -3 && 3 < forecast.Temperature.Value)
            {
                probabilities = Probabilities.Slippery;
            }
            else
            {
                probabilities = Probabilities.NotEvaluated;
            }
            return probabilities;
        }

        private Others GetOthers(HourlyForecast forecast)
        {
            Others others;
            
            if (forecast.RealFeelTemperature.Value >= 15 
                && forecast.IsDaylight == true 
                && forecast.Visibility.Value > 8 
                && forecast.PreciptationProbability < 30 
                && forecast.Wind.Speed.Value < 9 
                && forecast.WindGust.Speed.Value < 9)
            {
                others = Others.ADayAtTheParkOrWalking;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Rain.Value > 10 
                && forecast.Temperature.Value >-3 && 3 < forecast.Temperature.Value 
                && forecast.Visibility.Value < 5)
            {
                others = Others.SlipperyForDriving;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Snow.Value > 10 
                && forecast.Temperature.Value == 0 
                && forecast.Wind.Speed.Value > 15 
                && forecast.Wind.Speed.Value > 15 
                && forecast.Visibility.Value < 5)
            {
                others = Others.PoorDrivingConditions;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Temperature.Value < -10
                || forecast.Snow.Value > 10
                && forecast.Temperature.Value < -10)
            {
                others = Others.PreferableToDrive;
            }
            else
            {
                others = Others.NotEvaluated;
            }

            if (forecast.Temperature.Value > -6 && -1 < forecast.Temperature.Value
                && forecast.Snow.Value > 1 && 6 < forecast.Snow.Value)
            {
                others = Others.GoodForSki;
            }
            else
            {
                others = Others.NotEvaluated;
            }
            return others;
        }


       

        //private WeatherCondition GetWeatherCondition(CurrentForecast forecast)
        //{
        //    WeatherCondition weatherCondition;

        //    if (forecast.Rain)
        //}

    }
}
