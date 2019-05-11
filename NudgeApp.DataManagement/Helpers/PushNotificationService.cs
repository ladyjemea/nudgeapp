namespace NudgeApp.DataManagement.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using WebPush;

    public class PushNotificationService : IPushNotificationService
    {
        private const string publicKey = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";
        private const string privateKey = "zGZk1mpuMvOdjCedmOCjn8RYfIq4AXks-RNN8zeDlf0";

        private readonly ISubscritionRepository PushNotificationRepository;
        private readonly INotificationService NotificationService;

        public PushNotificationService(ISubscritionRepository pushNotificationRepository, INotificationService notificationService)
        {
            this.PushNotificationRepository = pushNotificationRepository;
            this.NotificationService = notificationService;
        }

        public void PushToUser(Guid userId, string title, string message)
        {
            var userSubscriptions = this.PushNotificationRepository.GetAll().Where(p => p.UserId == userId).ToList();

            foreach (var subscription in userSubscriptions)
            {
               this.SendNotification(subscription, title, message, "www.google.com");
            }
        }

        public void PushAll()
        {
            var subscriptions = this.PushNotificationRepository.GetAll();

            foreach (var sub in subscriptions)
            {
               this.SendNotification(sub, "NudgeApp News", "It is a good day to have some fun in the sun!", "");
            }
        }

        private void SendNotification(PushNotificationSubscriptionEntity pushNotificationEntity, string title, string message, string link)
        {
            var pushEndpoint = pushNotificationEntity.Endpoint;
            var p256dh = pushNotificationEntity.P256DH;
            var auth = pushNotificationEntity.Auth;

            var subject = "mailto:ccr008@uit.no";

            var subscription = new PushSubscription(pushEndpoint, p256dh, auth);
            var vapidDetails = new VapidDetails(subject, PushNotificationService.publicKey, PushNotificationService.privateKey);
            //var gcmAPIKey = @"[your key here]";
            var notificationPayload = new NotificationPayload
            {
                notification = new Notification
                {
                    title = title,
                    body = message,
                    data = new NotificationData
                    {
                        dateOfArrival = DateTime.Now,
                        PrimaryKey = 1,
                        Url = link
                    },
                    actions = new List<NotificationActions>
                        {
                            new NotificationActions
                            {
                                action = link,
                                title = "Accept Nudege",
                            }
                        }.ToArray()
                }
            };


            string json = JsonConvert.SerializeObject(notificationPayload);

            var webPushClient = new WebPushClient();
            try
            {
                webPushClient.SendNotification(subscription, json, vapidDetails);
                //webPushClient.SendNotification(subscription, "payload", gcmAPIKey);
            }
            catch (WebPushException exception)
            {
                Console.WriteLine("Http STATUS code" + exception.StatusCode);
            }
        }

        private class NotificationActions
        {
            public string action { get; set; }
            public string title { get; set; }
            
        }

        private class NotificationData
        {
            public DateTime dateOfArrival { get; set; }
            public int PrimaryKey { get; set; }
            public string Url { get; set; }
        }

        private class Notification
        {
            public string title { get; set; }
            public string body { get; set; }
            public NotificationData data { get; set; }
            public NotificationActions[] actions { get; set; }
        }

        private class NotificationPayload
        {
            public Notification notification { get; set; }
        }
    }
}
