namespace NudgeApp.DataAnalysis.Implementation
{
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class AnalysisService : IAnalysisService
    {
        private readonly IWeatherService WeatherService;
        private readonly INudgeRepository NudgeRepository;
        private readonly INudgeOracleRepository nudgeOracleRepository;


        public AnalysisService(IWeatherService weather, INudgeRepository nudge, INudgeOracleRepository nudgeOracle)
        {
            this.WeatherService = weather;
            this.NudgeRepository = nudge;
            this.nudgeOracleRepository = nudgeOracle;
        }

        public async Task<bool> AnalyseUser(Guid userId)
        {
            var nudges = this.NudgeRepository.GetAll().Where(nudge => nudge.UserId == userId).ToList();
            var weather = await this.WeatherService.GetCurrentForecast();
            var nudgeOracle = this.nudgeOracleRepository.ApproxCount(new QueryFilter
            {
                Result = Common.Enums.NudgeResult.Successful,
                MinTemperature = (int)weather.RawData.Temperature - 5,
                MaxTemperature = (int)weather.RawData.Temperature + 5
            });

            var filteredList = nudges.Where(nudge => nudge.Temperature > (int)weather.Temperature - 5 && nudge.Temperature < (int)weather.Temperature + 5);

            var success = filteredList.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Successful);
            var fail = filteredList.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Failed);
            var result_success = filteredList.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Successful);
            var result_fail = filteredList.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Failed);


            if (success > fail)
            {
                return true;
            }
            else if (result_success > result_fail)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool Duration(Guid userId, int tripDuration)
        {
            var nudges = this.NudgeRepository.GetAll().Where(nudge => nudge.UserId == userId).ToList();

            var nudgeOracle = this.nudgeOracleRepository.ApproxCount(new QueryFilter
            {
                Result= Common.Enums.NudgeResult.Successful,
                ActualTransportationType = Common.Enums.TransportationType.Walk
            });

            var durationfilter = nudges.Where(nudge => nudge.TransportationType == Common.Enums.TransportationType.Walk && (nudge.Duration > tripDuration - 10 && nudge.Duration < tripDuration + 10));

            var success = durationfilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Successful);
            var fail = durationfilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Failed);
            var result_success = durationfilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Successful);
            var result_fail = durationfilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Failed);

            if (success > fail)
            {
                return true;
            }
            else if (result_success > result_fail)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public bool Distance(Guid userId, int tripDistance)
        {
            var nudges = this.NudgeRepository.GetAll().Where(nudge => nudge.UserId == userId).ToList();
            var nudgeOracle = this.nudgeOracleRepository.ApproxCount(new QueryFilter
            {
                Result = Common.Enums.NudgeResult.Successful,
                ActualTransportationType = Common.Enums.TransportationType.Walk
            });

            var distancefilter = nudges.Where(nudge => nudge.TransportationType == Common.Enums.TransportationType.Walk && nudge.Distance > tripDistance- 10 && nudge.Distance < tripDistance + 10);

            var success = distancefilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Successful);
            var fail = distancefilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Failed);
            var result_success = distancefilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Successful);
            var result_fail = distancefilter.Count(n => n.NudgeResult == Common.Enums.NudgeResult.Failed);

            if (success > fail)
            {
                return true;
            }
            else if (result_success > result_fail)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
