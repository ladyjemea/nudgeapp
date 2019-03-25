namespace NudgeApp.DataAnalysis.Interfaces
{
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using System.Collections.Generic;
    using static NudgeApp.DataAnalysis.API.Analyzer;

    public interface IAnalyzer
    {
        IList<DateInfo> AnalyseWeather();
    }
}