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

        public PreferencesEntity AddPreferences(Guid userId)
        {
            var preferences = new PreferencesEntity
            {
                ActualTransportationType = TransportationType.Bus,
                AimedTransportationType = TransportationType.Bike,
                PreferedTransportationType = TransportationType.Bike,
                UserId = userId
            };

            this.Insert(preferences);

            return preferences;
        }

        public PreferencesEntity GetPreferences(Guid userId)
        {
            return this.Context.GetAll<PreferencesEntity>().Where(p => p.UserId == userId).FirstOrDefault();
        }
    }
}
