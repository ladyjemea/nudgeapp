namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NudgeApp.Data.Entities.Generic;

    public interface IRepository<TEntity> where TEntity : IDbEntity
    {
        TEntity Get(Guid id);
        Guid Insert(TEntity entity);
        void InsertWIthNoSave(TEntity entity);
        void Update(TEntity entity);
        IQueryable<TEntity> GetAll();
    }
}