namespace NudgeApp.Data.Entities
{
    using System;
    using NudgeApp.Data.Entities.Generic;

    public class NotificationEntity : DbEntity
    {
        public NotificationStatus Status { get; set; }
        public string Text { get; set; }
        public virtual NudgeEntity Nudge { get; set; }
        public Guid NudgeId { get; set; }
    }
}
