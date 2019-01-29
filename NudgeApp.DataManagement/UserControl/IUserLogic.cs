namespace NudgeApp.DataManagement.UserControl
{
    public interface IUserLogic
    {
        bool CreateUser(string username, string password);
        bool CheckPassword(string userName, string password);
    }
}