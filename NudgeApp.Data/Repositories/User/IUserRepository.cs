namespace NudgeApp.Data.Repositories.User
{
    using System;
    using NudgeApp.Data.Entities;

    public interface IUserRepository
    {
        Guid CreateUser(string userName, string passwordHash, string name, string email, string address);
        void UpdateUser(UserEntity user);

        UserEntity GetUser(string userName);
    }
}