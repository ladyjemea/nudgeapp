﻿namespace NudgeApp.Data.OracleDb.Queries
{
    public interface IAnonymousNudgesRepository
    {
        void GetAllEntries();
        void Insert(AnonymousNudgeEntity entity);
    }
}