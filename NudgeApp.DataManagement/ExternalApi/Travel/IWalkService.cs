namespace NudgeApp.DataManagement.ExternalApi.Travel
{
    using System.Threading.Tasks;
    using NudgeApp.Common.Dtos;
    using NudgeApp.DataManagement.ExternalApi.Bus.HelperObjects;

    public interface IWalkService
    {
        Task<RootObject> WalkInfo(Coordinates from, Coordinates to);
        Task<RootObject> WalkInfo(Coordinates from, string to);
    }
}