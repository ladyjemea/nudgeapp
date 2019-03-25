namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class PreferencesEntity : DbEntity
    {
        public TransportationType PreferedTransportationType { get; set; }
        public TransportationType ActualTransportationType { get; set; }
        public TransportationType AimedTransportationType { get; set; }        
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}
