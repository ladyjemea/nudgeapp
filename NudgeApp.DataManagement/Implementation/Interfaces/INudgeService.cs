namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Dtos;
    using System;

    public interface INudgeService
    {
        void AddNudge(Guid userId, TransportationType transportationType, WeatherDto forecast, TripDto trip);
        void Test();
    }
}