namespace NudgeApp.Data.Repositories.Interfaces
{
    using NudgeApp.Data.Entities;

    public interface IUserRepository : IRepository<UserEntity>
    {
        UserEntity GetUser(string userName);
    }
}