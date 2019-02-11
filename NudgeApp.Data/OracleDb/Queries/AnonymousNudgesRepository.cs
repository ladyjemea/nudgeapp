namespace NudgeApp.Data.OracleDb.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    public class AnonymousNudgesRepository : IAnonymousNudgesRepository
    {
        private readonly INudgeOracleConnection NudgeOracleConnection;

        private const string select = "SELECT * FROM \"Nudges\"";
        private const string insert = "INSERT INTO Nudges";

        public AnonymousNudgesRepository(INudgeOracleConnection nudgeOracleConnection)
        {
            this.NudgeOracleConnection = nudgeOracleConnection;
        }

        public void GetAllEntries()
        {
            this.NudgeOracleConnection.Command(select);
        }

        public void Insert(AnonymousNudgeEntity entity)
        {
            var query = insert + " VALUES (";
                        
            query += BitConverter.ToInt64(entity.Id.ToByteArray(), 0) + ", ";
            query += (int) entity.Result + ", ";
            query += (int) entity.UserPreferedTransportationType + ", ";
            query += (int) entity.ActualTransportationType + ", ";
            query += (int) entity.SkyCoverage + ", ";
            query += (int) entity.Road + ", ";
            query += entity.Temperature + ", ";
            query += entity.Wind + ", ";
            query += entity.Precipitation;

            query += ")";

            this.NudgeOracleConnection.Command(query);
        }

    }
}
