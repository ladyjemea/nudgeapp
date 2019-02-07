namespace NudgeApp.Data.OracleDb.Queries
{
    using NudgeApp.Common.Enums;
    using System;

    public class AnonymousNudgeEntity
    {
        public AnonymousNudgeEntity()
        {
            this.Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
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
