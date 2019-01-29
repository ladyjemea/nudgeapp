using NudgeApp.Data.Entities;
using System;
using System.Linq;

namespace NudgeApp.Data.Repositories.User
{
    public class UserRepository : IUserRepository
    {
        private readonly INudgeDbContext Db;

        public UserRepository(NudgeDbContext db)
        {
            this.Db = db;
        }

        public Guid CreateUser(string userName, string passwordHash)
        {
            var user = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = userName,
                PasswordHash = passwordHash
            };

            var result = this.Db.UserEntity.Add(user);

            this.Db.SaveChanges();

            return result.Entity.Id;
        }

        public UserEntity GetUser(string userName)
        {
            return this.Db.GetAll<UserEntity>().Where(u => u.UserName == userName).FirstOrDefault();
        }
    }
}
