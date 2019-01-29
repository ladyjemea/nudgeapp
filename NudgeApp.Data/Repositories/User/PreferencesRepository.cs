namespace NudgeApp.Data.Repositories.User
{
    using System;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;

    public class PreferencesRepository : IPreferencesRepository
    {
        private readonly INudgeDbContext Db;
        public PreferencesRepository(NudgeDbContext db)
        {
            this.Db = db;
        }

        public PreferencesEntity UpdatePreferences(Guid userId)
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
    }
}
