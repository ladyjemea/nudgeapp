namespace NudgeApp.Data.Entities
{
    using NudgeApp.Common.Enums;
    using System;

    public class AnonymousNudgeEntity : DbEntity
    {
        public NudgeResult Result { get; set; }
        public TransportationType UserPreferedTransportationType { get; set; }
        public TransportationType ActualTransportationType { get; set; }
        public SkyCoverageType SkyCoverage { get; set; }
        public RoadStateType Road { get; set; }
        public int Temperature { get; set; }
        public int Wind { get; set; }
        public int Precipitation { get; set; }
    }
}
