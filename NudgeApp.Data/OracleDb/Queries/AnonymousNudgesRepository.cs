namespace NudgeApp.Data.OracleDb.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Data.Entities;

    public class AnonymousNudgesRepository : IAnonymousNudgesRepository
    {
        private readonly INudgeOracleConnection NudgeOracleConnection;

        private const string select = "SELECT * FROM Nudges";
        private const string insert = "INSERT INTO Nudges";
        private const string approx_count = "APPROX_COUNT_DISTINCT From Nudges";

        public AnonymousNudgesRepository(INudgeOracleConnection nudgeOracleConnection)
        {
            this.NudgeOracleConnection = nudgeOracleConnection;
        }

        public void GetEntries(QueryFilter filter)
        {
            var query = select + " where ";
            var queryList = new List<string>();
            PropertyInfo[] properties = typeof(QueryFilter).GetProperties();
            foreach (PropertyInfo property in properties)
            {
                var value = property.GetValue(filter);
                if (value != null)
                {
                    var name = property.Name;
                    if (name.StartsWith("Min"))
                    {
                        name = name.Remove(0, 3);
                        queryList.Add(name + " >= " + (int)value);
                    }
                    else
                    if (name.StartsWith("Max"))
                    {
                        name = name.Remove(0, 3);
                        queryList.Add(name + " <= " + (int)value);
                    }
                    else
                    {
                        queryList.Add(name + " = " + (int)value);
                    }
                    
                    queryList.Add(" AND ");
                }
            }

            if (queryList.Count > 0)
            {
                queryList.RemoveAt(queryList.Count - 1);
                foreach (var queryFilter in queryList)
                {
                    query += queryFilter;
                }

                this.NudgeOracleConnection.SelectCommand(query);
            }
            else
            {
                throw new Exception("There are no elements to flilter by");
            }

        }

        public void GetAllEntries()
        {
            this.NudgeOracleConnection.InsertCommand(select);
        }

        public void Insert(AnonymousNudgeEntity entity)
        {
            GetEntries(new QueryFilter
            {
                MaxWind = 20,
                MinPrecipitation = 10,
                Result = Common.Enums.NudgeResult.Failed
            });
            var query = insert + " VALUES (";

            query += BitConverter.ToInt64(entity.Id.ToByteArray(), 0) + ", ";
            query += (int)entity.Result + ", ";
            query += (int)entity.UserPreferedTransportationType + ", ";
            query += (int)entity.ActualTransportationType + ", ";
            query += (int)entity.SkyCoverage + ", ";
            query += (int)entity.Road + ", ";
            query += entity.Temperature + ", ";
            query += entity.Wind + ", ";
            query += entity.Precipitation;

            query += ")";

            this.NudgeOracleConnection.InsertCommand(query);
        }

        public void SelectAll()
        {
            this.NudgeOracleConnection.SelectCommand(select);
        }
    }
}
