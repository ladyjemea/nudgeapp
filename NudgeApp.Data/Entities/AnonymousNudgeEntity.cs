namespace NudgeApp.Data.Entities
{
    using NudgeApp.Common.Enums;

    public class AnonymousNudgeEntity : DbEntity
    {
        public NudgeResult Result { get; set; }
        public TransportationType UserPreferedTransportationType { get; set; }
        public TransportationType ActualTransportationType { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public float Temperature { get; set; }
        public float Wind { get; set; }
        public int PrecipitationProbability { get; set; }
    }
}
