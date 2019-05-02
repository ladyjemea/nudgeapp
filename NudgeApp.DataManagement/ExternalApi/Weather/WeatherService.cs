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
    using NudgeApp.DataManagement.Helpers;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using RestSharp;

    public class WeatherService : IWeatherService
    {
        private const string APIkey = "XysYFpuGxOXOJNf6zXLk6fAy7pnH1yCr";
        private const string Locationkey = "256116"; // Tromsø 
        private const string serviceString = "http://dataservice.accuweather.com";

        private readonly IMemoryCacheService MemoryCacheService;
        private readonly IAnalysisConversion AnalysisConversion;

        public WeatherService(IMemoryCacheService memoryCacheService, IAnalysisConversion analysisConversion)
        {
            this.MemoryCacheService = memoryCacheService;
            this.AnalysisConversion = analysisConversion;
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
                RoadCondition = this.AnalysisConversion.GetRoadCondition(forecast),
                SkyCoverage = this.AnalysisConversion.GetSkyCoverage(forecast.CloudCover),
                PrecipitationCondition = this.AnalysisConversion.GetPrecipitation(forecast),
                WeatherCondition = this.AnalysisConversion.GetWeatherCondition(forecast),
                Others = this.AnalysisConversion.GetOthers(forecast),
                Probabilities = this.AnalysisConversion.GetProbabilities(forecast)
            };

            return weatherDto;
        }

        public async Task<WeatherDto> GetCurrentForecast()
        {
            var key = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, DateTime.UtcNow.Day, DateTime.UtcNow.Hour, 0, 0).ToString();
            var weatherDto = await this.MemoryCacheService.GetAsync<WeatherDto>(key);

            if (weatherDto == null)
            {
                weatherDto = await this.GetAndConvert();

                await this.MemoryCacheService.SaveAsync(key, weatherDto);
            }

            return weatherDto;
        }


        public async Task<WeatherDto> GetCurrentForecastRandom()
        {
            var key = Guid.NewGuid().ToString();
            var weatherDto = await this.MemoryCacheService.GetAsync<WeatherDto>(key);

            if (weatherDto == null)
            {
                weatherDto = await this.GetAndConvert();

                await this.MemoryCacheService.SaveAsync(key, weatherDto);
            }

            return weatherDto;
        }

        private async Task<WeatherDto> GetAndConvert()
        {
            var forecastList = await this.GetCurrentTromsForecast();
            var forecast = forecastList.First();

            var weatherDto = new WeatherDto
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

    }
}
