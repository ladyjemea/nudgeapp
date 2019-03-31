namespace NudgeApp.Data.Entities
{
    using NudgeApp.Data.Entities.Generic;

    public class UserEntity : DbEntity
    {
        public UserEntity() { }

        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public bool Google { get; set; }
    }
}
