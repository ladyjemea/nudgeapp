namespace NudgeApp.Data.Repositories
{
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class AnonymousNudgeRepository : Repository<OracleNudgeEntity>, IAnonymousNudgeRepository
    {
        public AnonymousNudgeRepository(INudgeDbContext context) : base(context) { }
    }
}
