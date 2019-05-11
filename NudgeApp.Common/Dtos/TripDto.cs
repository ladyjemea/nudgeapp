namespace NudgeApp.Common.Dtos
{
    using System;
    using NudgeApp.Common.Enums;

    public class TripDto
    {
        public TransportationType TransportationType { get; set; }
        public int Distance { get; set; }
        public int Duration { get; set; }
    }
}
