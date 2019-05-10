namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.Data.Repositories.Interfaces;

    public class NudgeRepository : Repository<NudgeEntity>, INudgeRepository
    {
        public NudgeRepository(INudgeDbContext context) : base(context) { }

        public Guid Insert(NudgeResult result, Guid userId, WeatherDto forecast, TripDto trip)
        {
            var entity = new NudgeEntity
            {
                UserId = userId,
                NudgeResult = result,
                TransportationType = trip.TransportationType,
                Distance = trip.Distance,
                Duration = trip.Duration.Minutes,
                SkyCoverage = forecast.SkyCoverage,
                Probability = forecast.Probabilities,
                ReafFeelTemperature = forecast.RealFeelTemperature,
                Temperature = forecast.Temperature,
                RoadCondition = forecast.RoadCondition,
                DateTime = forecast.DateTime,
                WindCondition = forecast.WindCondition
            };

            this.Insert(entity);
            return entity.Id;
        }

        public async Task GetApprox(QueryFilter queryFilter = null)
        {
            var filter = this.AddQuery(queryFilter);
            filter = (filter == string.Empty) ? string.Empty : (" where " + filter);

#pragma warning disable EF1000 // Possible SQL injection vulnerability.
            var result = await this.Context.Database.ExecuteSqlCommandAsync("SELECT APPROX_COUNT_DISTINCT(USERID) FROM Nudges" + filter);
#pragma warning restore EF1000 // Possible SQL injection vulnerability.

            Console.WriteLine(result);
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
