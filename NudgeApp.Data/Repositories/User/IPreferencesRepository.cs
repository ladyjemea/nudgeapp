namespace NudgeApp.Data.Repositories.User
{
    using System;
    using NudgeApp.Data.Entities;
    public interface IPreferencesRepository
    {
        PreferencesEntity UpdatePreferences(Guid userId);
    }
}