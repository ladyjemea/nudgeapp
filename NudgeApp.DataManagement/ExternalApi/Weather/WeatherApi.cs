namespace NudgeApp.DataManagement.ExternalApi.Weather
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using RestSharp;

    public class WeatherApi : IWeatherApi
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
            var client = new RestClient(serviceString);
            var request = new RestRequest("forecasts/v1/hourly/24hour/" + Locationkey, Method.GET);
            request.AddParameter("apikey", APIkey);
            request.AddParameter("language", "en-us");
            request.AddParameter("details", true);
            request.AddParameter("metric", true);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<HourlyForecast>>(response.Content);
        }

        public ForecastDto GetForecast(DateTime dateTime)
        {
            if (DateTime.Now.AddDays(1) > dateTime)
                throw new Exception("Date too far in the future. Forecast unreliable.");

            var forecast = this.Get24HTromsWeather().First(f => f.DateTime == dateTime);

            return new ForecastDto
            {
                CloudCoveragePercent = forecast.CloudCover,
                Temperature = forecast.RealFeelTemperature.Value,
                Time = dateTime,
                Wind = forecast.Wind.Speed.Value,
                PrecipitationProbability = forecast.PrecipitationProbability,
                SkyCoverage = GetSkyCoverage(forecast),
                RoadCondition = GetRoadCondition(forecast)
            };
        }

        private SkyCoverageType GetSkyCoverage(HourlyForecast forecast)
        {
            SkyCoverageType coverage = forecast.CloudCover <= 15 ? SkyCoverageType.Clear : SkyCoverageType.PartlyCloudy;

            if (forecast.CloudCover > 75) coverage = SkyCoverageType.Cloudy;

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
