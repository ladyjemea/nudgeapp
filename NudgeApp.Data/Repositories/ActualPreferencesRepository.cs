namespace NudgeApp.Data.Repositories
{
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class ActualPreferencesRepository : Repository<ActualPreferencesEntity>, IActualPreferencesRepository
    {
        public ActualPreferencesRepository(INudgeDbContext context) : base(context) { }
    }
}
