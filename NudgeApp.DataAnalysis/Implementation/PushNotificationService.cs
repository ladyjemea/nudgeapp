namespace NudgeApp.DataAnalysis.Implementation
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
    using NudgeApp.Data.Repositories.Interfaces;
    using WebPush;

    public class PushNotificationService : IPushNotificationService
    {
        private const string publicKey = "BD6e5GSCe5_Y08GgTyWlpFcQIPuMkLrEYfAiNBzrc-vkxPuYN3oeJqdvR3gjIGn_VxNu1G58J9zxbsd6-6FR70Y";
        private const string privateKey = "zGZk1mpuMvOdjCedmOCjn8RYfIq4AXks-RNN8zeDlf0";

        private readonly IPushNotificationRepository PushNotificationRepository;

        public PushNotificationService(IPushNotificationRepository pushNotificationRepository)
        {
            this.PushNotificationRepository = pushNotificationRepository;
        }

        public void PushAll()
        {
            var subscriptions = this.PushNotificationRepository.GetAll();

            foreach (var sub in subscriptions)
            {
                var pushEndpoint = sub.Endpoint;
                var p256dh = sub.P256DH;
                var auth = sub.Auth;

                var subject = "mailto:ccr008@uit.no";

                var subscription = new PushSubscription(pushEndpoint, p256dh, auth);
                var vapidDetails = new VapidDetails(subject, PushNotificationService.publicKey, PushNotificationService.privateKey);
                //var gcmAPIKey = @"[your key here]";
                var notificationPayload = new NotificationPayload
                {
                    notification = new Notification
                    {
                        title = "Angular news!",
                        body = "Newsletter available!",
                        data = new NotificationData
                        {
                            dateOfArrival = DateTime.Now,
                            PrimaryKey = 1
                        },
                        actions = new List<NotificationActions>
                        {
                            new NotificationActions
                            {
                                action = "explore",
                                title = "Go to the site"
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
