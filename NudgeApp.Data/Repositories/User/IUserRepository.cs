namespace NudgeApp.Data.Repositories.User
{
    using System;

    public interface IUserRepository
    {
        Guid CreateUser(string userName, string passwordHash);
    }
}