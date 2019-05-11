namespace NudgeApp.Data.Repositories
{
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class NotificationRepository : Repository<NotificationEntity>, INotificationRepository
    {
        public NotificationRepository(INudgeDbContext context) : base(context)
        {
        }
    }
}
