namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class PreferencesRepository : Repository<PreferencesEntity>, IPreferencesRepository
    {
        public PreferencesRepository(INudgeDbContext context) : base(context) { }
        
        public PreferencesEntity GetPreferences(Guid userId)
        {
            return this.Context.GetAll<PreferencesEntity>().Where(p => p.UserId == userId).FirstOrDefault();
        }
    }
}
