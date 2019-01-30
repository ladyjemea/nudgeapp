namespace NudgeApp.Common.Dtos
{
    using System;
    using NudgeApp.Common.Enums;

    public class EnvironmelntalInfoDto
    {
        public int Temperature { get; set; }
        public int CloudCoveragePercent { get; set; }
        public int Wind { get; set; }
        public RoadCondition RoadCondition { get; set; }
        public DateTime Time { get; set; }
    }
}
