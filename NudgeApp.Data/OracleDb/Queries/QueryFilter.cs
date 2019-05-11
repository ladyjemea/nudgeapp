namespace NudgeApp.Data.OracleDb.Queries
{
    using System;
    using NudgeApp.Common.Enums;

    public class QueryFilter
    {
        public NudgeResult? Result { get; set; }
        public TransportationType? UserPreferedTransportationType { get; set; }
        public TransportationType? ActualTransportationType { get; set; }
        public SkyCoverageType? SkyCoverage { get; set; }
        public RoadStateType? Road { get; set; }
        public int? MinTemperature { get; set; }
        public int? MaxTemperature { get; set; }
        public int? MinWind { get; set; }
        public int? MaxWind { get; set; }
        public int? MinPrecipitation { get; set; }
        public int? MaxPrecipitation { get; set; }
        public DateTime? MinDate { get; set; }
        public DateTime? MaxDate { get; set; }
        public DayOfWeek? WeekDay { get; set; }
    }
}
