namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class PushNotificationRepository : Repository<PushNotificationEntity>, IPushNotificationRepository
    {
        public PushNotificationRepository(INudgeDbContext context) : base(context) { }

        public Guid Create(Guid userId, string endpoint, string p256dh, string auth)
        {
            var entity = new PushNotificationEntity
            {
                UserId = userId,
                Endpoint = endpoint,
                P256DH = p256dh,
                Auth = auth
            };

            this.Insert(entity);

            return entity.Id;
        }

        public IList<PushNotificationEntity> GetAll()
        {
            return this.Context.GetAll<PushNotificationEntity>().ToList();
        }
    }
}
