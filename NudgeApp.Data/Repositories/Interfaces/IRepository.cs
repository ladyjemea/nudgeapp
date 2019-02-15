namespace NudgeApp.Data.Repositories.Interfaces
{
    using NudgeApp.Data.Entities;
    public interface IRepository<TEntity> where TEntity : IDbEntity
    {
        void Insert(TEntity entity);
        void InsertWIthNoSave(TEntity entity);
    }
}