namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;
    using System;

    [Serializable]
    public class WeatherDto
    {
        public DateTime DateTime { get; set; }
        public float Temperature { get; set; }
        public float RealFeelTemperature { get; set; }
        public float Ceiling { get; set; }
        public float Wind { get; set; }
        public int PrecipitationProbability { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public  WindCondition WindCondition { get; set; }
        public PrecipitationCondition PrecipitationCondition { get; set; }
        public WeatherCondition WeatherCondition { get; set; }
        public Probabilities Probabilities { get; set; }
        public Others Others { get; set; }
    }
}
