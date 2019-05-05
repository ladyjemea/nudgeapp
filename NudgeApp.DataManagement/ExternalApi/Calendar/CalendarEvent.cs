namespace NudgeApp.DataManagement.ExternalApi.Calendar
{
    using System;

    public class CalendarEvent
    {
        public string Summary { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
