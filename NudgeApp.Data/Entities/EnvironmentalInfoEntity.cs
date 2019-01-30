namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;

    public class EnvironmentalInfoEntity : DbEntity
    {
        public int Temperature { get; set; }
        public int CloudCoveragePercent { get; set; }
        public int Wind { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public DateTime Time { get; set; }
    }
}
