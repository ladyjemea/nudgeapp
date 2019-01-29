namespace NudgeApp.Data.Repositories.User
{
    using System;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Data.Entities;
    public interface IPreferencesRepository
    {
        PreferencesEntity AddPreferences(Guid userId);
        void UpdatePreferences(PreferencesEntity preferencesId);
        PreferencesEntity GetPreferences(Guid userId);
    }
}