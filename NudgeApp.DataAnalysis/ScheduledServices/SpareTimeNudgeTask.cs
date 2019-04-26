namespace NudgeApp.DataAnalysis.ScheduledServices
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NudgeApp.Common.Enums;
    using NudgeApp.DataAnalysis.Implementation;
    using NudgeApp.DataAnalysis.ScheduledServices.TaskScheduler;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class SpareTimeNudgeTask : ScheduledProcessor
    {
        private readonly ILogger<SpareTimeNudgeTask> Logger;
        
        public SpareTimeNudgeTask(IServiceScopeFactory serviceScopeFactory, ILogger<SpareTimeNudgeTask> logger) : base(serviceScopeFactory)
        {
            this.Logger = logger;
        }

        protected override string Schedule => "* * * * * 6";

        public async override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            this.Logger.LogInformation($"Spare time nudge running at {DateTime.UtcNow} UTC.");

            var weatherService = serviceProvider.GetService<IWeatherService>();
            var forecast =  await weatherService.GetCurrentForecast();

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
        }
    }
}
