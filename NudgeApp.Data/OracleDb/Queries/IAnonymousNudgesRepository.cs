using NudgeApp.Data.Entities;

namespace NudgeApp.Data.OracleDb.Queries
{
    public interface IAnonymousNudgeOracleRepository
    {
        void GetAllEntries();
        void Insert(AnonymousNudgeEntity entity);
        void SelectAll();
    }
}