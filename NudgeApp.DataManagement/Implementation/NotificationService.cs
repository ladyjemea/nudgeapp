namespace NudgeApp.DataManagement.Implementation
{
    using Microsoft.EntityFrameworkCore;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class NotificationService : INotificationService
    {
        private readonly ISubscritionRepository SubscritionRepository;
        private readonly INotificationRepository NotificationRepository;
        private readonly INudgeRepository NudgeRepository;

        public NotificationService(ISubscritionRepository subscritionRepository, INotificationRepository notificationRepository, INudgeRepository nudgeRepository)
        {
            this.SubscritionRepository = subscritionRepository;
            this.NotificationRepository = notificationRepository;
            this.NudgeRepository = nudgeRepository;
        }

        public IList<Notification> GetAllNotifications(Guid userId)
        {
            return this.NotificationRepository.GetAll()
                .Include(notification => notification.Nudge)
                .Where(notification => notification.Nudge.UserId == userId)
                .Select(notification =>
                    new Notification
                    {
                        Id = notification.Id,
                        Status = notification.Status,
                        CreatedOn = notification.CreatedOn,
                        Text = notification.Text,
                        NudgeResult = notification.Nudge.NudgeResult
                    })
                .OrderBy(not => not.CreatedOn)
                .ToList();
        }

        public void SetNudgeResult(Guid notificationId, NudgeResult nudgeResult)
        {
            var notification = this.NotificationRepository.Get(notificationId);
            var nudge = this.NudgeRepository.Get(notification.NudgeId);

            nudge.NudgeResult = nudgeResult;
            notification.Status = NotificationStatus.Set;

            this.NudgeRepository.Update(nudge);
            this.NotificationRepository.Update(notification);
        }

        public void SetSubscription(Guid userId, PushSubscription pushSubscription)
        {
            var notification = this.SubscritionRepository.GetAll().FirstOrDefault(p => p.UserId == userId);

            if (notification == null)
            {
                this.SubscritionRepository.Create(userId, pushSubscription.Endpoint, pushSubscription.P256DH, pushSubscription.Auth);
            }
            else
            {
                if (pushSubscription.Endpoint != notification.Endpoint)
                {
                    notification.Auth = pushSubscription.Auth;
                    notification.P256DH = pushSubscription.P256DH;
                    notification.Endpoint = pushSubscription.Endpoint;
                    this.SubscritionRepository.Update(notification);
                }
            }
        }
    }

    public class Notification
    {
        public Guid Id { get; set; }
        public NotificationStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
        public NudgeResult NudgeResult { get; set; }

    }

    public class PushSubscription
    {
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
    }
}
