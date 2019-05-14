namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class NudgeEntity : DbEntity
    {
        public NudgeResult Result { get; set; }
        public TransportationType TransportationType { get; set; }
        public TripType Type { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public float ReafFeelTemperature { get; set; }
        public float Temperature { get; set; }
        public float Wind { get; set; }
        public int PrecipitationProbability { get; set; }
        public int CloudCoveragePercent { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public WindCondition WindCondition { get; set; }
        public Probabilities WeatherProbability { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public DateTime DateTime { get; set; }

        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}
