namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;

    public class PreferencesEntity : DbEntity
    {
        public TravelTypes PreferedTravelType { get; set; }
        public TravelTypes ActualTravelType { get; set; }
        public TravelTypes AimedTransportationType { get; set; }        
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
    }
}
