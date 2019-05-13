namespace NudgeApp.Common.Dtos
{
    using System;

    public class UserEvent
    {
        public string Location { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public string Name { get; set; }
    }
}
