namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Enums;
    public interface IUserLogic
    {
        bool CreateUser(string username, string password, string name, string email, string address);
        bool CheckPassword(string userName, string password);

        void UpdateUserPreferences(string userName, TravelTypes travelType);
    }
}