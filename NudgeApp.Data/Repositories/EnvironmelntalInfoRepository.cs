namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class EnvironmelntalInfoRepository : Repository<WeatherForecastEntity>, IEnvironmelntalInfoRepository
    {
        public EnvironmelntalInfoRepository(INudgeDbContext context) : base(context) { }

        public Guid CreateInfo(ForecastDto forecast)
        {
            var entity = new WeatherForecastEntity
            {
                CloudCoveragePercent = forecast.CloudCoveragePercent,
                RoadCondition = forecast.RoadCondition,
                Temperature = forecast.Temperature,
                Time = forecast.Time,
                Wind = forecast.Wind
            };

            this.Insert(entity);

            return entity.Id;
        }

        public ForecastDto GetForecast(Guid id)
        {
            var entity = this.Get(id);

            ForecastDto result = null;
            if (entity != null)
            {
                result = new ForecastDto
                {
                    CloudCoveragePercent = entity.CloudCoveragePercent,
                    RoadCondition = entity.RoadCondition,
                    Temperature = entity.Temperature,
                    Time = entity.Time,
                    Wind = entity.Wind
                };
            }

            return result;
        }
    }
}
