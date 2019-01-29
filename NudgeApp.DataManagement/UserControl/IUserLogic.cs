namespace NudgeApp.DataManagement.UserControl
{
    public interface IUserLogic
    {
        void CreateUser(string username, string password);
        bool CheckPassword(string userName, string password);
    }
}