namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities.Generic;
    using NudgeApp.Data.Repositories.Interfaces;

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : DbEntity
    {
        protected readonly INudgeDbContext Context;
        private readonly DbSet<TEntity> DbSet;

        protected Repository(INudgeDbContext context)
        {
            this.Context = context;
            this.DbSet = this.Context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            InsertWIthNoSave(entity);
            this.Context.SaveChanges();
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

            entity.Modified = DateTime.UtcNow;
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
    }
}
