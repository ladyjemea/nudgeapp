namespace NudgeApp.Data
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Entities.Generic;

    public class NudgeDbContext : DbContext, INudgeDbContext
    {
        public const string connectionString = @"Server=localhost; Database=NudgeAppDatabase; Trusted_Connection = True;";
        //public const string connectionString = @"Server=localhost\MSSQLSERVER01; Database=NudgeAppDatabase; Trusted_Connection = True;"; 
        public const string onlineConnectionString = @"Server=nudgeapp.cz12dzdjwlt5.eu-central-1.rds.amazonaws.com,1433; Database=NudgeAppDatabase; Trusted_Connection = False; uid=nudgeuser; pwd=46AEcnT5RPPe4Mcu";

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PreferencesEntity> Preferences { get; set; }
        public DbSet<NudgeEntity> Nudges { get; set; }
        public DbSet<PushNotificationEntity> PushNotifications { get; set; }
        public DbSet<NotificationEntity> Notifications { get; set; }
        public DbSet<OracleNudgeEntity> AnonymousNudges { get; set; }

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
            modelBuilder.Entity<OracleNudgeEntity>();

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
