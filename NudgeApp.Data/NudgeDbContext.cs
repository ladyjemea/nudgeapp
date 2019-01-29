namespace NudgeApp.Data
{
    using DatabaseDesign;
    using Microsoft.EntityFrameworkCore;

    public class NudgeDbContext : DbContext, INudgeDbContext
    {
        public const string connectionString = "Server=localhost; Database=NudgeAppDatabase; Trusted_Connection = True;";

        public DbSet<UserEntity> UserEntity { get; set; }

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

            base.OnModelCreating(modelBuilder);
        }
    }
}
