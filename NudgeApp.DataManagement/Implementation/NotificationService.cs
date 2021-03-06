﻿namespace NudgeApp.DataManagement.Implementation
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Logging;
    using NudgeApp.Common.Dtos;
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
        private readonly ILogger<NotificationService> logger;

        public NotificationService(ILogger<NotificationService> logger,  ISubscritionRepository subscritionRepository, INotificationRepository notificationRepository, INudgeRepository nudgeRepository)
        {
            this.SubscritionRepository = subscritionRepository;
            this.NotificationRepository = notificationRepository;
            this.NudgeRepository = nudgeRepository;
            this.logger = logger;
        }

        public void Insert(string message, Guid nudgeId)
        {
            this.NotificationRepository.Insert(new NotificationEntity
            {
                NudgeId = nudgeId,
                Text = message
            });
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
                        CreatedOn = notification.CreatedOn,
                        Text = notification.Text,
                        NudgeResult = notification.Nudge.Result
                    })
                .OrderByDescending(not => not.CreatedOn)
                .ToList();
        }

        public NotificationDto GetNudgeNotification(Guid notificationId)
        {
            var notification = this.NotificationRepository.GetAll()
                .Include(not => not.Nudge)
                .FirstOrDefault(not => not.Id == notificationId);

            var notificationDto = new NotificationDto
            {
                Id = notification.Id,
                Text = notification.Text,
                CreatedOn = notification.CreatedOn,
                NudgeResult = notification.Nudge.Result,
                CloudCoveragePercent = notification.Nudge.CloudCoveragePercent,
                DateTime = notification.Nudge.DateTime,
                Distance = notification.Nudge.Distance,
                Duration = notification.Nudge.Duration,
                PrecipitationProbability = notification.Nudge.PrecipitationProbability,
                Probability = notification.Nudge.WeatherProbability,
                ReafFeelTemperature = notification.Nudge.ReafFeelTemperature,
                RoadCondition = notification.Nudge.RoadCondition,
                SkyCoverage = notification.Nudge.SkyCoverage,
                Temperature = notification.Nudge.Temperature,
                TransportationType = notification.Nudge.TransportationType,
                TripType = notification.Nudge.Type,
                Wind = notification.Nudge.Wind,
                WindCondition = notification.Nudge.WindCondition
            };

            return notificationDto;
        }

        public void SetNudgeResult(Guid notificationId, NudgeResult nudgeResult)
        {
            if (notificationId == Guid.Empty)
            {
                this.logger.LogWarning("Guid is empty");

                return;
            }

            var notification = this.NotificationRepository.Get(notificationId);
            var nudge = this.NudgeRepository.Get(notification.NudgeId);

            nudge.Result = nudgeResult;

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
