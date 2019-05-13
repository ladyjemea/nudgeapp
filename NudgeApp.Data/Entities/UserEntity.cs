namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Data.Entities.Generic;

    public class UserEntity : DbEntity
    {
        public UserEntity() { }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public virtual AccountEntity Account { get; set; }
        public Guid AccountId { get; set; }
    }
}
