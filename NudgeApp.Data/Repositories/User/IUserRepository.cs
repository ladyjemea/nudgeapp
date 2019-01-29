namespace NudgeApp.Data.Repositories.User
{
    using System;
    using NudgeApp.Data.Entities;

    public interface IUserRepository
    {
        Guid CreateUser(string userName, string passwordHash);

        UserEntity GetUser(string userName);
    }
}