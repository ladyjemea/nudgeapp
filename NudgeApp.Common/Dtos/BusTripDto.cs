using NudgeApp.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace NudgeApp.Common.Dtos
{
    public class BusTripDto
    {
        public TimeSpan Duration { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public int ChangeNb { get; set; }
        public Coordinates StartCoordinates { get; set; }
        public Coordinates EndCoordinates { get; set; }
        public SortedList<int, TravelPart> TravelParts { get; set; }

        public BusTripDto()
        {
            this.TravelParts = new SortedList<int, TravelPart>();
        }
    }

    public class TravelPart
    {
        public string DepartureName { get; set; }
        public string ArrivalName { get; set; }
        public TransportationType Type { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
