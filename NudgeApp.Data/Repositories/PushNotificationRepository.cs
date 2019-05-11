namespace NudgeApp.Data.Repositories
{
    using System;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class SubscriptionRepository : Repository<PushNotificationSubscriptionEntity>, ISubscritionRepository
    {
        public SubscriptionRepository(INudgeDbContext context) : base(context) { }

        public Guid Create(Guid userId, string endpoint, string p256dh, string auth)
        {
            var entity = new PushNotificationSubscriptionEntity
            {
                UserId = userId,
                Endpoint = endpoint,
                P256DH = p256dh,
                Auth = auth
            };

            this.Insert(entity);

            return entity.Id;
        }
    }
}
