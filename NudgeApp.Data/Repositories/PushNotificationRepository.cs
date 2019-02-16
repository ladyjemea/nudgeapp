namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class PushNotificationRepository : IPushNotificationRepository
    {
        private readonly INudgeDbContext Db;
        public PushNotificationRepository(INudgeDbContext db)
        {
            this.Db = db;
        }

        public Guid Create (Guid userId, string endpoint, string p256dh, string auth)
        {
            var entity = new PushNotificationEntity
            {
                UserId = userId,
                Endpoint = endpoint,
                P256DH = p256dh,
                Auth = auth
            };

            var result = this.Db.PushNotificationEntity.Add(entity);
            this.Db.SaveChanges();

            return result.Entity.Id;
        }

        public void Update(PushNotificationEntity entity)
        {
            this.Db.PushNotificationEntity.Update(entity);
            this.Db.SaveChanges();
        }

        public IList<PushNotificationEntity> GetAll()
        {
            return this.Db.GetAll<PushNotificationEntity>().ToList();
        }

        public PushNotificationEntity Get(Guid userId)
        {
            var entity = this.Db.GetAll<PushNotificationEntity>().Where(p => p.UserId == userId).FirstOrDefault();
            
            return entity;
        }
    }
}
