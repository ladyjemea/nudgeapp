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

        public Guid CreateInfo(WeatherDto forecast)
        {
            var entity = new WeatherForecastEntity
            {
                CloudCoveragePercent = forecast.RawData.CloudCoveragePercent,
                RoadCondition = forecast.RoadCondition,
                Temperature = forecast.RawData.Temperature,
                Time = forecast.RawData.Time,
                Wind = forecast.RawData.Wind
            };

            this.Insert(entity);

            return entity.Id;
        }

        public WeatherDto GetForecast(Guid id)
        {
            var entity = this.Get(id);

            WeatherDto result = null;
            if (entity != null)
            {
                result = new WeatherDto
                {
                    CloudCoveragePercent = entity.CloudCoveragePercent,
                    RoadCondition = entity.RoadCondition,
                    RawData = new WeatherRawData
                    {
                        Temperature = entity.Temperature,
                        Time = entity.Time,
                        Wind = entity.Wind
                    }
                };
            }

            return result;
        }
    }
}
