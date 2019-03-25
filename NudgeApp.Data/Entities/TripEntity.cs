namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class TripEntity : DbEntity
    {
        public virtual UserEntity User { get; set; }
        public Guid UserId { get; set; }
        public TransportationType UsedTransportationType { get; set; }
        public int DistanceTraveled { get; set; }
        public virtual WeatherForecastEntity WeatherForecast { get; set; }
        public Guid WeatherForecastId { get; set; }
    }
}
