namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private readonly INudgeDbContext Db;

        public UserRepository(INudgeDbContext context) : base(context)
        {
            this.Db = context;
        }

        public void UpdateUser(UserEntity user)
        {
            this.Db.UserEntity.Update(user);
            this.Db.SaveChanges();
        }

        public UserEntity GetUser(string userName)
        {
            return this.Db.GetAll<UserEntity>().Where(u => u.UserName == userName).FirstOrDefault();
        }
    }
}
