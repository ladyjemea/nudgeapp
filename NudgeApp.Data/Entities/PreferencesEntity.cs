namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class PreferencesEntity : DbEntity
    {
        public TransportationType TransportationType { get; set; }
        public int MinTemperature { get; set; }
        public int MaxTemperature { get; set; }
        public bool RainyTrip { get; set; }
        public bool SnowyTrip { get; set; }
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}
