namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;
    using System;

    public interface IUserService
    {
        bool CreateUser(string username, string password, string name, string email, string address, TransportationType travelType);
        UserEntity CheckPassword(string userName, string password);
        void GetUser();
        Guid VerifyGoogle(string id, string tokenId);
    }
}