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
    using RestSharp;

    public class WeatherService : IWeatherService
    {
        private const string APIkey = "XysYFpuGxOXOJNf6zXLk6fAy7pnH1yCr";
        private const string Locationkey = "256116"; // Tromsø 
        private const string serviceString = "http://dataservice.accuweather.com";

        private readonly IMemoryCacheService MemoryCacheService;
        private readonly IDataAgregator DataAgregator;

        public WeatherService(IMemoryCacheService memoryCacheService, IDataAgregator dataAgregator)
        {
            this.MemoryCacheService = memoryCacheService;
            this.DataAgregator = dataAgregator;
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
        
        public WeatherDto GetForecast(DateTime dateTime)
        {
            if (DateTime.Now.AddHours(12) < dateTime)
                throw new Exception("Date too far in the future. Forecast unreliable.");

            var longForecast = this.Get12HTromsWeather();
            var forecast = longForecast.First(f => f.DateTime.Hour == dateTime.Hour);

            var weatherDto = new WeatherDto
            {
                DateTime = dateTime,
                RealFeelTemperature = forecast.RealFeelTemperature.Value,
                Temperature = forecast.Temperature.Value,
                Wind = forecast.Wind.Speed.Value,
                Ceiling = forecast.Ceiling.Value,
                PrecipitationProbability = forecast.PrecipitationProbability,
                RoadCondition = this.DataAgregator.GetRoadCondition(forecast),
                SkyCoverage = this.DataAgregator.GetSkyCoverage(forecast.CloudCover),
                PrecipitationCondition = this.DataAgregator.GetPrecipitation(forecast),
                WeatherCondition = this.DataAgregator.GetWeatherCondition(forecast),
                Others = this.DataAgregator.GetOthers(forecast),
                Probabilities = this.DataAgregator.GetProbabilities(forecast),
                WindCondition = this.DataAgregator.GetWindCondition(forecast)
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
                    DateTime = DateTime.Now,
                    RealFeelTemperature = forecast.RealFeelTemperature.Metric.Value,
                    Temperature = forecast.Temperature.Metric.Value,
                    Wind = forecast.Wind.Speed.Value,
                    Ceiling = forecast.Ceiling.Metric.Value,
                    PrecipitationProbability = forecast.HasPrecipitation ? 100 : 0,
                    SkyCoverage = this.DataAgregator.GetSkyCoverage(forecast.CloudCover),
                    PrecipitationCondition = this.DataAgregator.GetPrecipitation(forecast),
                    WeatherCondition = this.DataAgregator.GetWeatherCondition(forecast),
                    Others = this.DataAgregator.GetOthers(forecast),
                    Probabilities = this.DataAgregator.GetProbabilities(forecast),
                    WindCondition = this.DataAgregator.GetWindCondition(forecast),
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

    }
}
