﻿namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities.Generic;
    using NudgeApp.Data.Repositories.Interfaces;

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : DbEntity
    {
        protected readonly INudgeDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(INudgeDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<TEntity>();
        }

        public virtual Guid Insert(TEntity entity)
        {
            InsertWIthNoSave(entity);
            this.Context.SaveChanges();

            return entity.Id;
        }

        public virtual void InsertWIthNoSave(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        public virtual void Update(TEntity entity)
        {
            var local = this.Context.Set<TEntity>()
                    .Local
                    .FirstOrDefault(entry => entry.Id.Equals(entity.Id));
            if (local != null)
            {
                this.Context.Entry(local).State = EntityState.Detached;
            }

            entity.ModifiedOn = DateTime.UtcNow;
            this.DbSet.Update(entity);
            this.Context.SaveChanges();
        }

        protected virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            this.Context.SaveChanges();
        }

        public virtual TEntity Get(Guid id)
        {
            return this.DbSet.Find(id);
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return this.DbSet.AsQueryable();
        }
    }
}
