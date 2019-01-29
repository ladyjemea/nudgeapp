namespace NudgeApp.Data.Repositories.User
{
    using System;
    using System.Linq;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;

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
                ActualTravelType = TravelTypes.Bus,
                AimedTransportationType = TravelTypes.Bike,
                PreferedTravelType = TravelTypes.Bike,
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
