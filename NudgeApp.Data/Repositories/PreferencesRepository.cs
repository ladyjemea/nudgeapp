namespace NudgeApp.Data.Repositories
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using NudgeApp.Data.Repositories.Interfaces;

    public class PreferencesRepository : IPreferencesRepository
    {
        private readonly INudgeDbContext Db;
        public PreferencesRepository(NudgeDbContext db)
        {
            this.Db = db;
        }

        public PreferencesEntity AddPreferences(Guid userId)
        {
            var preferences = new PreferencesEntity
            {
                ActualTravelType = Common.Enums.TravelTypes.Bus,
                AimedTransportationType = Common.Enums.TravelTypes.Bike,
                PreferedTravelType = Common.Enums.TravelTypes.Bike,
                UserId = userId
            };

            var result = this.Db.PreferencesEntity.Add(preferences);
            this.Db.SaveChanges();

            return result.Entity;
        }

        public void UpdatePreferences(PreferencesEntity preferences)
        {
            this.Db.PreferencesEntity.Update(preferences);
            this.Db.SaveChanges();
        }

        public PreferencesEntity GetPreferences(Guid userId)
        {
            return this.Db.GetAll<PreferencesEntity>().Where(p => p.UserId == userId).FirstOrDefault();
        }
    }
}
