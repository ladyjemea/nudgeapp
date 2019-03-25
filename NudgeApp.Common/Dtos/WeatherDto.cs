namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;
    using System;

    public class WeatherDto
    {
        public WeatherRawData RawData { get; set; }
        
        public RoadCondition RoadCondition { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public int CloudCoveragePercent { get; set; }
    }

    public class WeatherRawData
    {
        public DateTime Date { get; set; }
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float RealFeelTemperature { get; set; }
        public float Ceiling { get; set; }
        public float Rain { get; set; }
        public int RainProbability { get; set; }
        public float Snow { get; set; }
        public int SnowProbability { get; set; }
        public float Ice { get; set; }
        public int IceProbability { get; set; }
        public float Visibility { get; set; }
        public float Wind { get; set; }
        public bool Daylight { get; set; }
        public int PrecipitationProbability { get; set; }
        public int CloudCoveragePercent { get; set; }
    }
}
