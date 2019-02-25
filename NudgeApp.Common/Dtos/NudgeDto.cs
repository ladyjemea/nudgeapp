namespace NudgeApp.Common.Dtos
{
    using System;
    using NudgeApp.Common.Enums;

    public class NudgeDto
    {
        public NudgeResult NudgeResult { get; set; }
        public TransportationType TransportationType { get; set; }
        public DateTime DepartureTime { get; set; }
    }
}
