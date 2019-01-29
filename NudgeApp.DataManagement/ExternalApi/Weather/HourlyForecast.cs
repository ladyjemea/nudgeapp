namespace NudgeApp.DataManagement.ExternalApi.Weather
{
    using System;

    public class HourlyForecast
    {
        public DateTime DateTime { get; set; }
        public long EpochDateTime { get; set; }
        public int WeatherIcon { get; set; }
        public string IconPhrase { get; set; }
        public bool IsDaylight { get; set; }
        public WeatherInfo Temperature { get; set; }
        public WeatherInfo RealFeelTemperature { get; set; }
        public WeatherInfo WetBulbTemperature { get; set; }
        public WeatherInfo DewPoint { get; set; }
        public WindInfo Wind { get; set; }
        public WindInfo WindGust { get; set; }
        public int RelativeHumidity { get; set; }
        public WeatherInfo Visibility { get; set; }
        public WeatherInfo Ceiling { get; set; }
        public int UVIndex { get; set; }
        public string UVIndexText { get; set; }
        public int PrecipitationProbability { get; set; }
        public int RainProbability { get; set; }
        public int SnowProbability { get; set; }
        public int IceProbability { get; set; }
        public WeatherInfo TotalLiquid { get; set; }
        public WeatherInfo Rain { get; set; }
        public WeatherInfo Snow { get; set; }
        public WeatherInfo Ice { get; set; }
        public int CloudCover { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class WindInfo
    {
        public int Degrees { get; set; }
        public string Localized { get; set; }
        public string English { get; set; }
    }

    public class WeatherInfo
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public string UnitType { get; set; }
    }
}

