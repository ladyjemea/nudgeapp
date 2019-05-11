using NudgeApp.Common.Enums;
using NudgeApp.DataManagement.ExternalApi.Weather;

namespace NudgeApp.DataManagement.Helpers
{
    public interface IDataAgregator
    {
        SkyCoverageType GetSkyCoverage(int cloudCoverPercentage);

        WindCondition GetWindCondition(HourlyForecast forecast);
        RoadCondition GetRoadCondition(HourlyForecast forecast);
        PrecipitationCondition GetPrecipitation(HourlyForecast forecast);
        WeatherCondition GetWeatherCondition(HourlyForecast forecast);
        Probabilities GetProbabilities(HourlyForecast forecast);
        Others GetOthers(HourlyForecast forecast);

        WindCondition GetWindCondition(CurrentForecast forecast);
        PrecipitationCondition GetPrecipitation(CurrentForecast forecast);
        WeatherCondition GetWeatherCondition(CurrentForecast forecast);
        Probabilities GetProbabilities(CurrentForecast forecast);
        Others GetOthers(CurrentForecast forecast);
    }
}