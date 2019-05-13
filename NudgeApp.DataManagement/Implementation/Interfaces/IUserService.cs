namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using System;
    using System.Collections.Generic;

    public interface IUserService
    {
        bool CreateUser(string password, string name, string email, string address, TransportationType travelType);
        UserEntity CheckPassword(string userName, string password);
        IList<Guid> GetAllUserIds();
        Guid VerifyGoogle(string id, string tokenId);
        void UpdatePreferences(Guid userId, PreferencesDto preferencesDto);
        PreferencesDto GetPreferences(Guid userId);
    }
}