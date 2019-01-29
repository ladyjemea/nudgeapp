namespace NudgeApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities;

    public interface INudgeDbContext
    {
        DbSet<UserEntity> UserEntity { get; set; }

        int SaveChanges();
    }
}
