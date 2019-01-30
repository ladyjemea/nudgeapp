namespace NudgeApp.Data
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities;

    public interface INudgeDbContext
    {
        DbSet<UserEntity> UserEntity { get; set; }
        DbSet<PreferencesEntity> PreferencesEntity { get; set; }
        DbSet<NudgeEntity> NudgeEntity { get; set; }
        DbSet<EnvironmentalInfoEntity> EnvironmentalInfoEntity { get; set; }
        DbSet<TripEntity> TripEntity { get; set; }

        int SaveChanges();

        IQueryable<T> GetAll<T>() where T : class, IDbEntity;
    }
}
