namespace NudgeApp.Data.Repositories
{
    using System;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : DbEntity
    {
        private readonly INudgeDbContext context;
        private readonly DbSet<TEntity> DbSet;

        protected Repository(INudgeDbContext ct)
        {
            this.context = ct;
            this.DbSet = this.context.Set<TEntity>();
        }

        public virtual void Insert(TEntity entity)
        {
            InsertWIthNoSave(entity);
            this.context.SaveChanges();
        }

        public virtual void InsertWIthNoSave(TEntity entity)
        {
            this.DbSet.Add(entity);
        }

        protected virtual void Update(TEntity entity)
        {
            entity.Modified = DateTime.UtcNow;
            this.DbSet.Update(entity);
            this.context.SaveChanges();
        }

        protected virtual void Delete(TEntity entity)
        {
            this.DbSet.Remove(entity);
            this.context.SaveChanges();
        }

        public virtual TEntity Get(Guid id)
        {
            return this.DbSet.Find(id);
        }
    }
}
