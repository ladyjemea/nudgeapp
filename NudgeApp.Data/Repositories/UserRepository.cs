namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class UserRepository : Repository<UserEntity>, IUserRepository
    {
        private readonly INudgeDbContext Db;

        public UserRepository(INudgeDbContext context) : base(context)
        {
            this.Db = context;
        }

        public UserEntity GetUser(string email)
        {
            return this.Db.GetAll<UserEntity>().Include(u => u.Account).Where(u => u.Email == email).FirstOrDefault();
        }

        public IEnumerable<Guid> GetAllIds()
        {
            return this.GetAll().Select(u => u.Id);
        }
    }
}
