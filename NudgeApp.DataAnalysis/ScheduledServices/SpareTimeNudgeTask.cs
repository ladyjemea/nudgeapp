namespace NudgeApp.DataAnalysis.ScheduledServices
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NudgeApp.DataAnalysis.ScheduledServices.TaskScheduler;
    using NudgeApp.DataManagement.ExternalApi.Calendar;
    using System;
    using System.Threading.Tasks;

    public class SpareTimeNudgeTask : TimeTaskScheduler
    {
        private readonly ILogger<SpareTimeNudgeTask> Logger;
        
        public SpareTimeNudgeTask(IServiceScopeFactory serviceScopeFactory, ILogger<SpareTimeNudgeTask> logger) : base(serviceScopeFactory)
        {
            this.Logger = logger;
        }

        protected override string Schedule => "* * * 1 * *";

        public async override Task ScheduledTask(IServiceProvider serviceProvider)
        {
            this.Logger.LogInformation($"Spare time nudge running at {DateTime.UtcNow} UTC.");

            var isWeekend = DateTime.Now.DayOfWeek == DayOfWeek.Saturday || DateTime.Now.DayOfWeek == DayOfWeek.Sunday;
            var publicEvent = await serviceProvider.GetService<IPublicCalendarsService>().GetTodaysEvent();

            if(isWeekend || publicEvent != null)
            {
                // do stuff
            }




            /* Example: 
             * if (forecast.Temperature > 15 && fdsaknflka)
             {
                 var succesfullNudges = nudgeRepository.ApproxCount(new QueryFilter
                 {
                     Result = NudgeResult.Successful,
                     MinTemperature = (int)(forecast.Temperature) - 5,
                     MaxTemperature = (int)(forecast.Temperature) + 5,
                 });
                  var failed = nudgeRepository.ApproxCount(new QueryFilter
                 {
                     Result = NudgeResult.Failed,
                     MinTemperature = (int)(forecast.Temperature) - 5,
                     MaxTemperature = (int)(forecast.Temperature) + 5,
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
