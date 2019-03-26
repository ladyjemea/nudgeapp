namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class TripEntity : DbEntity
    {
        public TripType Type { get; set; }
        public TransportationType UsedTransportationType { get; set; }
        public int DistanceTraveled { get; set; }
        public virtual WeatherForecastEntity WeatherForecast { get; set; }
        public Guid WeatherForecastId { get; set; }
    }
}
