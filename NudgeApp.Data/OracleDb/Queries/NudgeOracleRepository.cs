namespace NudgeApp.Data.OracleDb.Queries
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using NudgeApp.Data.Entities;

    public class NudgeOracleRepository : INudgeOracleRepository
    {
        private readonly INudgeOracleConnection NudgeOracleConnection;

        private const string count_distinct = "SELECT COUNT(DISTINCT(USERID)) FROM Nudges";
        private const string approx_count_distinct = "SELECT APPROX_COUNT_DISTINCT(USERID) FROM Nudges";
        private const string approx_count = "SELECT APPROX_COUNT_DISTINCT(ID) FROM Nudges";
        private const string count = "SELECT Count(ID) FROM Nudges";
        private const string select = "SELECT * FROM Nudges";
        private const string insert = "INSERT INTO Nudges";

        public NudgeOracleRepository(INudgeOracleConnection nudgeOracleConnection)
        {
            this.NudgeOracleConnection = nudgeOracleConnection;
        }

       
        public void GetAllEntries()
        {
            this.NudgeOracleConnection.InsertCommand(select);
        }

        public void Insert(OracleNudgeEntity entity)
        {
            var query = insert + " VALUES (";

            query += BitConverter.ToInt64(entity.Id.ToByteArray(), 0) + ", ";
            query += (int)entity.Result + ", ";
            query += (int)entity.UserPreferedTransportationType + ", ";
            query += (int)entity.ActualTransportationType + ", ";
            query += (int)entity.SkyCoverage + ", ";
            query += (int)entity.RoadCondition + ", ";
            query += entity.Temperature + ", ";
            query += entity.Wind + ", ";
            query += entity.PrecipitationProbability + ", ";
            query += BitConverter.ToInt64(entity.UserId.ToByteArray(), 0);

            query += ")";

            this.NudgeOracleConnection.InsertCommand(query);
        }

        public void SelectAll()
        {
            this.NudgeOracleConnection.SelectCommand(select);
        }

        public (int, long) Count(QueryFilter queryFilter = null)
        {
            var filter = AddQuery(queryFilter);

            var query = count + ((filter == string.Empty) ? string.Empty : (" where " + filter));

            var watch = Stopwatch.StartNew();
            var result = this.NudgeOracleConnection.SelectCommand(query).First().Trim();
            watch.Stop();

            return (Convert.ToInt32(result), watch.ElapsedMilliseconds);
        }

        public (int, long) CountDistinct(QueryFilter queryFilter = null)
        {
            var filter = AddQuery(queryFilter);
           
            var query = count_distinct + ((filter == string.Empty) ? string.Empty : (" where " + filter));

            var watch = Stopwatch.StartNew();
            var result = this.NudgeOracleConnection.SelectCommand(query).First().Trim();
            watch.Stop();

            return (Convert.ToInt32(result), watch.ElapsedMilliseconds);
        }

        public (int result, long duration) ApproxCount(QueryFilter queryFilter = null)
        //public int ApproxCount(QueryFilter queryFilter = null)
        {
            var filter = AddQuery(queryFilter);

            var query = approx_count_distinct + ((filter == string.Empty) ? string.Empty : (" where " + filter));

            var watch = Stopwatch.StartNew();
            var result = this.NudgeOracleConnection.SelectCommand(query).First().Trim();
            watch.Stop();

            return (Convert.ToInt32(result), watch.ElapsedMilliseconds);
            //return Convert.ToInt32(result);
        }

        private string AddQuery(QueryFilter filter)
        {
            if (filter == null)
                return string.Empty;

            var query = string.Empty;
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
            }

            return query.Trim();
        }
    }
}
