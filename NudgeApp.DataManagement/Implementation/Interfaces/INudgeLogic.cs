namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Dtos;
    public interface INudgeLogic
    {
        void AddNudge(NudgeDto nudge, EnvironmelntalInfoDto envInfo, string userName);
    }
}