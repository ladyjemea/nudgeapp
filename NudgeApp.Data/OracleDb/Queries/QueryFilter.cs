namespace NudgeApp.Data.OracleDb.Queries
{
    using NudgeApp.Common.Enums;

    public class QueryFilter
    {
        public NudgeResult? Result_success { get; set; }
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
        public NudgeResult Result_fail { get; set; }
    }
}
