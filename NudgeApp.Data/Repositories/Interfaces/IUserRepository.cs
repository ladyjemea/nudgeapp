namespace NudgeApp.Data.Repositories.Interfaces
{
    using NudgeApp.Data.Entities;
    using System;
    using System.Collections.Generic;

    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetUser(string email);
        IEnumerable<Guid> GetAllIds();
    }
}