namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Data.Entities;

    public interface IPreferencesRepository : IRepository<PreferencesEntity>
    {
        PreferencesEntity AddPreferences(Guid userId);
        PreferencesEntity GetPreferences(Guid userId);
    }
}