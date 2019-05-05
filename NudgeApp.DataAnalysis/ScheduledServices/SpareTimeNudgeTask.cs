namespace NudgeApp.DataAnalysis.ScheduledServices
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.DataAnalysis.Implementation;
    using NudgeApp.DataAnalysis.ScheduledServices.TaskScheduler;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class SpareTimeNudgeTask : TimeTaskScheduler
    {
        private readonly ILogger<SpareTimeNudgeTask> Logger;
        
        public SpareTimeNudgeTask(IServiceScopeFactory serviceScopeFactory, ILogger<SpareTimeNudgeTask> logger) : base(serviceScopeFactory)
        {
            this.Logger = logger;
        }

        protected override string Schedule => "* * 10 * * 6";

        public override Task ScheduledTask(IServiceProvider serviceProvider)
        {
            this.Logger.LogInformation($"Spare time nudge running at {DateTime.UtcNow} UTC.");

            var weatherService = serviceProvider.GetService<IWeatherService>();
            var nudgeRepository = serviceProvider.GetService<INudgeOracleRepository>();

            //var forecast =  await weatherService.GetCurrentForecast();

            var userLogic = serviceProvider.GetService<IUserService>();
            var pushNotificationService = serviceProvider.GetService<IPushNotificationService>();

            var userIds = userLogic.GetAllUserIds();

            foreach (var userId in userIds)
            {
                pushNotificationService.PushToUser(userId, "Nudge of the day", "Hello");
            }

            return Task.FromResult(0);


            /* Example: 
             * if (forecast.RawData.Temperature > 15 && fdsaknflka)
             {
                 var succesfullNudges = nudgeRepository.ApproxCount(new QueryFilter
                 {
                     Result = NudgeResult.Successful,
                     MinTemperature = (int)(forecast.RawData.Temperature) - 5,
                     MaxTemperature = (int)(forecast.RawData.Temperature) + 5,
                 });
                  var failed = nudgeRepository.ApproxCount(new QueryFilter
                 {
                     Result = NudgeResult.Failed,
                     MinTemperature = (int)(forecast.RawData.Temperature) - 5,
                     MaxTemperature = (int)(forecast.RawData.Temperature) + 5,
                 });
             }*/
            /*
           if (forecast.WeatherCondition == WeatherCondition.StrongWinds)
           {
               var userLogic = serviceProvider.GetService<IUserService>();
               var pushNotificationService = serviceProvider.GetService<IPushNotificationService>();

               var userIds = userLogic.GetAllUserIds();

               foreach (var userId in userIds)
               {
                   pushNotificationService.PushToUser(userId, "Nudge of the day", "Too Windy to go out!");
               }
           }
           else if (forecast.Probabilities == Probabilities.Rain)
           {
               var userLogic = serviceProvider.GetService<IUserService>();
               var pushNotificationService = serviceProvider.GetService<IPushNotificationService>();

               var userIds = userLogic.GetAllUserIds();

               foreach (var userId in userIds)
               {
                   pushNotificationService.PushToUser(userId, "Nudge of the day", "It's Rainy");
               }
           }
           else
           {
               var userLogic = serviceProvider.GetService<IUserService>();
               var pushNotificationService = serviceProvider.GetService<IPushNotificationService>();

               var userIds = userLogic.GetAllUserIds();

               foreach (var userId in userIds)
               {
                   pushNotificationService.PushToUser(userId, "Nudge of the day", "Hello");
               }

           }*/
        }
    }
}
