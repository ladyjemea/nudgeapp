namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using System.Threading.Tasks;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.OracleDb.Queries;

    public interface INudgeRepository : IRepository<NudgeEntity>
    {
        Guid Insert(NudgeResult successful, Guid userId, WeatherDto forecast, TripDto trip);
        Task GetApprox(QueryFilter queryFilter = null);
    }
}