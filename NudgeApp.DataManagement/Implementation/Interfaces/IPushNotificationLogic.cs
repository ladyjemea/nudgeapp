namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using System;

    public interface IPushNotificationLogic
    {
        void SetSubscription(Guid userId, PushSubscription pushSubscription);
    }
}