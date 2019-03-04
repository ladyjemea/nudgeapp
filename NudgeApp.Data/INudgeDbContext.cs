namespace NudgeApp.Data
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.EntityFrameworkCore.Infrastructure;
    using NudgeApp.Data.Entities;

    public interface INudgeDbContext
    {
        DatabaseFacade Database { get; }
        DbSet<NudgeEntity> NudgeEntity { get; set; }
        DbSet<EnvironmentalInfoEntity> EnvironmentalInfoEntity { get; set; }
        DbSet<TripEntity> TripEntity { get; set; }
        DbSet<PushNotificationEntity> PushNotificationEntity { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        int SaveChanges();

        IQueryable<T> GetAll<T>() where T : class, IDbEntity;
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
