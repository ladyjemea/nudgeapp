using NudgeApp.Data.Entities;

namespace NudgeApp.Data.OracleDb.Queries
{
    public interface IAnonymousNudgeOracleRepository
    {
        void GetAllEntries();
        void Insert(AnonymousNudgeEntity entity);
        void SelectAll();
        (int, long) Count(QueryFilter queryFilter = null);
        (int, long) CountDistinct(QueryFilter queryFilter = null);
        (int, long) ApproxCount(QueryFilter queryFilter = null);
    }
}