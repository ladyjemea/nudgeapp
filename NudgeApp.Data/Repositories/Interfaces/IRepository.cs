namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Data.Entities;

    public interface IRepository<TEntity> where TEntity : IDbEntity
    {
        TEntity Get(Guid id);
        void Insert(TEntity entity);
        void InsertWIthNoSave(TEntity entity);
    }
}