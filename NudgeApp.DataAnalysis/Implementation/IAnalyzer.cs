namespace NudgeApp.DataAnalysis.API
{
    using NudgeApp.Common.Dtos;
    using System;
    using System.Threading.Tasks;

    public interface IAnalyzer
    {
        WeatherDto AnalyseWeather();
        Task AnalyseEvent(Guid userId, UserEvent userEvent, Coordinates userLocation);
    }
}