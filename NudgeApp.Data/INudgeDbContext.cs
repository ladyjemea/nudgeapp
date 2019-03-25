namespace NudgeApp.Data
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Entities.Generic;

    public interface INudgeDbContext
    {
        DatabaseFacade Database { get; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        IQueryable<T> GetAll<T>() where T : class, IDbEntity;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
