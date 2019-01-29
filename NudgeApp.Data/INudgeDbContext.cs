namespace NudgeApp.Data
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities;

    public interface INudgeDbContext
    {
        DbSet<UserEntity> UserEntity { get; set; }

        int SaveChanges();

        IQueryable<T> GetAll<T>() where T : class, IDbEntity;
    }
}
