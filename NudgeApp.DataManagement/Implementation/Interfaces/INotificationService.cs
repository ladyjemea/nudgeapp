namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using System;
    using System.Collections.Generic;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;

    public interface INotificationService
    {
        void Insert(string message, Guid nudgeId);
        void SetSubscription(Guid userId, PushSubscription pushSubscription);
        IList<Notification> GetAllNotifications(Guid userId);
        void SetNudgeResult(Guid notificationId, NudgeResult nudgeResult);
        NotificationDto GetNudgeNotification(Guid notificationId);
    }
}