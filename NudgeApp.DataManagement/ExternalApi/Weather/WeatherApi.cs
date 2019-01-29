namespace NudgeApp.DataManagement.ExternalApi.Weather
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using RestSharp;

    public class WeatherApi : IWeatherApi
    {
        private const string APIkey = "XysYFpuGxOXOJNf6zXLk6fAy7pnH1yCr";
        private const string Locationkey = "256116"; // Tromsø 

        public IList<HourlyForecast> GetTromsWeather()
        {
            var list = new List<HourlyForecast>();
            list.Add(new HourlyForecast
            {
                DateTime = DateTime.Now,
                RainProbability = 100
            });

            return list;

            var client = new RestClient("http://dataservice.accuweather.com");
            var request = new RestRequest("forecasts/v1/hourly/12hour/" + Locationkey, Method.GET);
            request.AddParameter("apikey", APIkey);
            request.AddParameter("language", "en-us");
            request.AddParameter("details", true);
            request.AddParameter("metric", true);

            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<List<HourlyForecast>>(response.Content);
        }
    }
}
