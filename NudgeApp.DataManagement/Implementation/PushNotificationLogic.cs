namespace NudgeApp.DataManagement.Implementation
{
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;

    public class PushNotificationLogic : IPushNotificationLogic
    {
        private readonly IPushNotificationRepository PushNotificationRepository;
        private readonly IUserRepository UserRepository;

        public PushNotificationLogic(IPushNotificationRepository pushNotificationRepository, IUserRepository userRepository)
        {
            this.PushNotificationRepository = pushNotificationRepository;
            this.UserRepository = userRepository;
        }

        public void SetSubscription(Guid userId, PushSubscription pushSubscription)
        {
            var notification = this.PushNotificationRepository.Get(userId);

            if (notification == null)
            {
                this.PushNotificationRepository.Create(userId, pushSubscription.Endpoint, pushSubscription.P256DH, pushSubscription.Auth);
            }
            else
            {
                if (pushSubscription.Endpoint != notification.Endpoint)
                {
                    notification.Auth = pushSubscription.Auth;
                    notification.P256DH = pushSubscription.P256DH;
                    notification.Endpoint = pushSubscription.Endpoint;
                    this.PushNotificationRepository.Update(notification);
                }
            }
        }
    }

    public class PushSubscription
    {
        public string Endpoint { get; set; }
        public string P256DH { get; set; }
        public string Auth { get; set; }
    }
}
