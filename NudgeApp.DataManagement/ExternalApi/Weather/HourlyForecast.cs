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
        public UnitInfo Temperature { get; set; }
        public UnitInfo RealFeelTemperature { get; set; }
        public UnitInfo WetBulbTemperature { get; set; }
        public UnitInfo DewPoint { get; set; }
        public WindInfo Wind { get; set; }
        public WindInfo WindGust { get; set; }
        public int RelativeHumidity { get; set; }
        public UnitInfo Visibility { get; set; }
        public UnitInfo Ceiling { get; set; }
        public int UVIndex { get; set; }
        public string UVIndexText { get; set; }
        public int PrecipitationProbability { get; set; }
        public int RainProbability { get; set; }
        public int SnowProbability { get; set; }
        public int IceProbability { get; set; }
        public UnitInfo TotalLiquid { get; set; }
        public UnitInfo Rain { get; set; }
        public UnitInfo Snow { get; set; }
        public UnitInfo Ice { get; set; }
        public int CloudCover { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
    }

    public class WindInfo
    {
        public UnitInfo Speed { get; set; }
        public Direction Direction { get; set; }
    }

    public class Direction
    {
        public int Degrees { get; set; }
        public string Localized { get; set; }
        public string English { get; set; }
    }

    public class UnitInfo
    {
        public float Value { get; set; }
        public string Unit { get; set; }
        public string UnitType { get; set; }
    }
}

