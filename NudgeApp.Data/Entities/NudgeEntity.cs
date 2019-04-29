namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class NudgeEntity : DbEntity
    {
        public NudgeResult NudgeResult { get; set; }
        public TransportationType TransportationType { get; set; }
        public TripType Type { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public DateTime Time { get; set; }
        public float Temperature { get; set; }
        public float ReafFeelTemperature { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public WeatherCondition Wind { get; set; }
        public Probabilities Probability { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}
