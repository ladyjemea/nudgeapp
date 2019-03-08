namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using System.Collections.Generic;
    using NudgeApp.Data.Entities;

    public interface IPushNotificationRepository : IRepository<PushNotificationEntity>
    {
        Guid Create(Guid userId, string endpoint, string p256dh, string auth);
        IList<PushNotificationEntity> GetAll();
    }
}