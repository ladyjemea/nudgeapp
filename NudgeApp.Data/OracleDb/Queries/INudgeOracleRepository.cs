using NudgeApp.Data.Entities;

namespace NudgeApp.Data.OracleDb.Queries
{
    public interface INudgeOracleRepository
    {
        void GetAllEntries();
        void Insert(OracleNudgeEntity entity);
        void SelectAll();
        (int, long) Count(QueryFilter queryFilter = null);
        (int, long) CountDistinct(QueryFilter queryFilter = null);
        (int result, long duration) ApproxCount(QueryFilter queryFilter = null);
        //int ApproxCount(QueryFilter queryFilter = null);
    }
}