namespace NudgeApp.Data.Entities
{
    using NudgeApp.Data.Entities.Generic;

    public class AccountEntity : DbEntity
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public bool Google { get; set; }
    }
}
