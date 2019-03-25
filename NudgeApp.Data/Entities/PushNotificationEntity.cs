namespace NudgeApp.Data.Entities
{
    using NudgeApp.Data.Entities.Generic;
    using System;

    public class PushNotificationEntity : DbEntity
    {
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
        public Guid UserId { get; set; }
    }
}
