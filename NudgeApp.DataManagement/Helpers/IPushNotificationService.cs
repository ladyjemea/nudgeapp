﻿namespace NudgeApp.DataManagement.Helpers
{
    using System;

    public interface IPushNotificationService
    {

        void PushToUser(Guid userId, string title, string message);
        void PushAll();
    }
}