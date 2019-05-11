namespace NudgeApp.Data.Repositories.Interfaces
{
    using System;
    using NudgeApp.Data.Entities;

    public interface IPreferencesRepository : IRepository<PreferencesEntity>
    {
        PreferencesEntity GetPreferences(Guid userId);
    }
}