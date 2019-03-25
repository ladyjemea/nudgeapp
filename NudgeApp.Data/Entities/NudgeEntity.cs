namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class NudgeEntity : DbEntity
    {
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
        public virtual WeatherForecastEntity WeatherForecast { get; set; }
        public Guid WeatherForecastId { get; set; }
        public NudgeResult NudgeResult { get; set; }
        public TransportationType TransportationType { get; set; }
    }
}
