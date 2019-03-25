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

    public class CurrentForecast
    {
        public DateTime LocalObservationDateTime { get; set; }
        public long EpochTime { get; set; }
        public string WeatherText { get; set; }
        public int WeatherIcon { get; set; }
        public LocalSourceInfo LocalSource { get; set; }
        public bool IsDayTime { get; set; }
        public UnitType Temperature { get; set; }
        public UnitType RealFeelTemperature { get; set; }
        public UnitType RealFeelTemperatureShade { get; set; }
        public int RelativeHumidity { get; set; }
        public UnitType DewPoint { get; set; }
        public WindInfo Wind { get; set; }
        public int UVIndex { get; set; }
        public string UVIndexText { get; set; }
        public UnitType Visibility { get; set; }
        public string ObstructionsToVisibility { get; set; }
        public int CloudCover { get; set; }
        public UnitType Ceiling { get; set; }
        public UnitType Pressure { get; set; }
        public PressureTendencyInfo PressureTendency { get; set; }
        public UnitType Past24HourTemperatureDeparture { get; set; }
        public UnitType ApparentTemperature { get; set; }
        public UnitType WindChillTemperature { get; set; }
        public UnitType WetBulbTemperature { get; set; }
        public UnitType Precip1hr { get; set; }
        public PrecipitationSummaryInfo PrecipitationSummary { get; set; }
        public string MobileLink { get; set; }
        public string Link { get; set; }
        public bool HasPrecipitation { get; set; }
        public string PrecipitationType { get; set; }
        public bool HasSnow { get; internal set; }
    }

    public class PrecipitationSummaryInfo
    {
        public UnitType PastHour { get; set; }
        public UnitType Past3Hours { get; set; }
        public UnitType Past6Hours { get; set; }
        public UnitType Past9Hours { get; set; }
        public UnitType Past12Hours { get; set; }
        public UnitType Past18Hours { get; set; }
        public UnitType Past24Hours { get; set; }
        public MinMax Past6HourRange { get; set; }
        public MinMax Past12HourRange { get; set; }
        public MinMax Past24HourRange { get; set; }
    }

    public class MinMax
    {
        public UnitType Minimum { get; set; }
        public UnitType Maximum { get; set; }
    }

    public class PressureTendencyInfo
    {
        string LocalizedText { get; set; }
        string Code { get; set; }
    }

    public class UnitType
    {
        public UnitInfo Metric { get; set; }
        public UnitInfo Imperial { get; set; }
    }

    public class LocalSourceInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string WeatherCode { get; set; }
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

