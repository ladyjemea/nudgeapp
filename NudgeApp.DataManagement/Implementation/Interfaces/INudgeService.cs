﻿namespace NudgeApp.DataManagement.Implementation.Interfaces
{
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using System;

    public interface INudgeService
    {
        void AddNudge(Guid userId, TransportationType transportationType, WeatherDto forecast, TripDto trip);
        void Test();
    }
}