namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class UserRepository : IUserRepository
    {
        private readonly INudgeDbContext Db;

        public UserRepository(INudgeDbContext db)
        {
            this.Db = db;
        }

        public Guid CreateUser(string userName, string passwordHash, string name, string email, string address)
        {
            var user = new UserEntity
            {
                UserName = userName,
                PasswordHash = passwordHash,
                Address = address,
                Email = email,
                Name = name
            };

            var result = this.Db.UserEntity.Add(user);

            this.Db.SaveChanges();

            return result.Entity.Id;
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
