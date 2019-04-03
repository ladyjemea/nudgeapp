namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;
    using System;

    [Serializable]
    public class WeatherDto
    {
        public WeatherRawData RawData { get; set; }
        
        public RoadCondition RoadCondition { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public PrecipitationCondition Precipitation { get; set; }
        public  WeatherCondition Windy { get; set; }
        public PrecipitationCondition PrecipitationCondition { get; set; }
        public WeatherCondition WeatherCondition { get; set; }
        public Probabilities Probabilities { get; set; }
        public Others Others { get; set; }
    }

    [Serializable]
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
        public int Precipitation { get; set; }
        public float WindGust { get; set; }
    }
}
