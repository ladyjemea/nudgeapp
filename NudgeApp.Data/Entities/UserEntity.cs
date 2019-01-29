namespace NudgeApp.Data.Entities
{
    using NudgeApp.Data;

    public class UserEntity : DbEntity
    {
        public UserEntity() { }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public int PreferedTravelType { get; set; }
        public int ActualTravelType { get; set; }
        public int AimedTransportationType { get; set; }
    }
}
