using System;
using NudgeApp.Common.Enums;

namespace NudgeApp.Common.Dtos
{
    public class NotificationDto
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public DateTime CreatedOn { get; set; }
        public NudgeResult NudgeResult { get; set; }
        public TransportationType TransportationType { get; set; }
        public TripType TripType { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
        public float ReafFeelTemperature { get; set; }
        public float Temperature { get; set; }
        public float Wind { get; set; }
        public int PrecipitationProbability { get; set; }
        public int CloudCoveragePercent { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public WindCondition WindCondition { get; set; }
        public Probabilities Probability { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public DateTime DateTime { get; set; }
    }
}