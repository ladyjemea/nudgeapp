namespace NudgeApp.Data.Entities
{
    using System;

    public class UserEntity : DbEntity
    {
        public UserEntity() { }

        public string UserName { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
    }
}
