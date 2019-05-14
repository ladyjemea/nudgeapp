namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities.Generic;

    public class NotificationEntity : DbEntity
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public virtual NudgeEntity Nudge { get; set; }
        public Guid NudgeId { get; set; }
    }
}
