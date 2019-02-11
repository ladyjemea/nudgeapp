namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    public interface IPushNotificationLogic
    {
        void SetSubscription(string username, PushSubscription pushSubscription);
    }
}