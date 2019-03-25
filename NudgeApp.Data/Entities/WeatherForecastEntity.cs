namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class WeatherForecastEntity : DbEntity
    {
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float ReafFeelTemperature { get; set; }
        public int CloudCoveragePercent { get; set; }
        public float Wind { get; set; }
        public int PrecipitationProbability { get; set; }
        public RoadCondition RoadCondition { get; set; }

        public NudgeEntity Nudge { get; set; }
    }
}
