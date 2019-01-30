namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;

    public class TripEntity : DbEntity
    {
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
        public TransportationType UsedTransportationType { get; set; }
        public int DistanceTraveled { get; set; }
        public virtual EnvironmentalInfoEntity EnvironmentalInfo { get; set; }
        public Guid EnvironmentalInfoId { get; set; }
    }
}
