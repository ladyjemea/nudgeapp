namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Entities;

    public interface IUserService
    {
        bool CreateUser(string username, string password, string name, string email, string address, TransportationType travelType);
        UserEntity CheckPassword(string userName, string password);
        void GetUser();
    }
}