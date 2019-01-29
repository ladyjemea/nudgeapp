namespace NudgeApp.DataManagement.UserControl
{
    using NudgeApp.Common.Dtos;
    public interface IUserLogic
    {
        bool CreateUser(string username, string password, string name, string email, string address);
        bool CheckPassword(string userName, string password);

        void UpdateUserPreferences(string userName, PreferencesDto preferencesDto);
    }
}