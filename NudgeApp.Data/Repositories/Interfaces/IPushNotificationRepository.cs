namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Data.Entities;

    public interface ISubscritionRepository : IRepository<PushNotificationSubscriptionEntity>
    {
        Guid Create(Guid userId, string endpoint, string p256dh, string auth);
    }
}