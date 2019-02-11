namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Data.Entities;

    public interface IPushNotificationRepository
    {
        Guid Create(Guid userId, string endpoint, string p256dh, string auth);
        void Update(PushNotificationEntity entity);
        PushNotificationEntity Get(Guid userId);
    }
}