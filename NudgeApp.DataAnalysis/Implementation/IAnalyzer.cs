using NudgeApp.Common.Dtos;
using NudgeApp.DataManagement.ExternalApi.Weather;
using System.Collections.Generic;
using static NudgeApp.DataAnalysis.API.Analyzer;

namespace NudgeApp.DataAnalysis.API
{
    public interface IAnalyzer
    {
        WeatherDto AnalyseWeather();
    }
}