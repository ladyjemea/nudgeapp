namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Enums;
    public interface IUserLogic
    {
        bool CreateUser(string username, string password, string name, string email, string address, TransportationType travelType);
        bool CheckPassword(string userName, string password);
    }
}