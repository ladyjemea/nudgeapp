namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;
    using System;

    public class ForecastDto
    {
        public SkyCoverageType SkyCoverage { get; set; }
        public int PrecipitationProbability { get; set; }
        public float Temperature { get; set; }
        public int CloudCoveragePercent { get; set; }
        public float Wind { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public DateTime Time { get; set; }
    }
}
