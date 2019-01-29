using DatabaseDesign;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace NudgeApp.Data
{
    interface INudgeDbContext
    {
        DbSet<UserEntity> UserEntity { get; set; }

        int SaveChanges();
    }
}
