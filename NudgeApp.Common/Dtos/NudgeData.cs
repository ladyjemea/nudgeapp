namespace NudgeApp.Common.Dtos
{
    using NudgeApp.Common.Enums;

    public class NudgeData
    {
        public TransportationType TransportationType { get; set; }
        public ForecastDto Forecast { get; set; }
        public TripDto Trip { get; set; }
    }
}
