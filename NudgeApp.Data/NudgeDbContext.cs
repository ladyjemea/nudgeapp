namespace NudgeApp.Data
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using NudgeApp.Data.Entities;

    public class NudgeDbContext : DbContext, INudgeDbContext
    {
        // to update the database run the commnand: dotnet ef database update
        public const string connectionString = "Server=localhost; Database=NudgeAppDatabase; Trusted_Connection = True;";

        public DbSet<UserEntity> UserEntity { get; set; }
        public DbSet<PreferencesEntity> PreferencesEntity { get; set; }
        public DbSet<NudgeEntity> NudgeEntity { get; set; }
        public DbSet<EnvironmentalInfoEntity> EnvironmentalInfoEntity { get; set; }
        public DbSet<TripEntity> TripEntity { get; set; }
        public DbSet<PushNotificationEntity> PushNotificationEntity { get; set; }

        public NudgeDbContext() : base() { }

        public NudgeDbContext(DbContextOptions options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.UseSqlServer(NudgeDbContext.connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>();
            modelBuilder.Entity<PreferencesEntity>();
            modelBuilder.Entity<NudgeEntity>();
            modelBuilder.Entity<EnvironmentalInfoEntity>();
            modelBuilder.Entity<TripEntity>();

            base.OnModelCreating(modelBuilder);
        }

        public IQueryable<T> GetAll<T>() where T : class, IDbEntity
        {
            return Set<T>().AsNoTracking();
        }

        public override EntityEntry Update(object entity)
        {
            ((DbEntity)entity).Modified = DateTime.UtcNow;

            return base.Update(entity);
        }
    }
}
