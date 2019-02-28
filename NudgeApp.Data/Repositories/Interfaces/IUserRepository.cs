namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Data.Entities;

    public interface IUserRepository : IRepository<UserEntity>
    {
        void UpdateUser(UserEntity user);

        UserEntity GetUser(string userName);
    }
}