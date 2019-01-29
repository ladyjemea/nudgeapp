namespace NudgeApp.Data.Entities
{
    using NudgeApp.Data;

    public class PreferencesEntity : DbEntity
    {
        public int PreferedTravelType { get; set; }
        public int ActualTravelType { get; set; }
        public int AimedTransportationType { get; set; }

        public UserEntity User { get; set; }
    }
}
