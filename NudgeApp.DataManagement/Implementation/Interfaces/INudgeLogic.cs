namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Dtos;
    using System;

    public interface INudgeLogic
    {
        void AddNudge(Guid userId, NudgeData nudgeData);
        void Test();
    }
}