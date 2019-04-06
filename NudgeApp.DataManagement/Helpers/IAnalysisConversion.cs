using NudgeApp.Common.Enums;
using NudgeApp.DataManagement.ExternalApi.Weather;

namespace NudgeApp.DataManagement.Helpers
{
    public interface IAnalysisConversion
    {
        SkyCoverageType GetSkyCoverage(int cloudCoverPercentage);
        RoadCondition GetRoadCondition(HourlyForecast forecast);
        PrecipitationCondition GetPrecipitation(HourlyForecast forecast);
        WeatherCondition GetWeatherCondition(HourlyForecast forecast);
        Probabilities GetProbabilities(HourlyForecast forecast);
        Others GetOthers(HourlyForecast forecast);
    }
}