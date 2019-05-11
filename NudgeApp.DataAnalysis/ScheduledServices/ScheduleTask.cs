namespace NudgeApp.DataAnalysis.ScheduledServices
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.Repositories.Interfaces;
    using NudgeApp.DataAnalysis.ScheduledServices.TaskScheduler;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Helpers;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using System;
    using System.Threading.Tasks;

    /* Example from:
     * https://thinkrethink.net/2018/05/31/run-scheduled-background-tasks-in-asp-net-core/
     * https://github.com/pgroene/ASPNETCoreScheduler
     *  ┌───────────── second (0 - 59)
     *    ┌───────────── minute (0 - 59)
     *    │ ┌───────────── hour (0 - 23)
     *    │ │ ┌───────────── day of month (1 - 31)
     *    │ │ │ ┌───────────── month (1 - 12)
     *    │ │ │ │ ┌───────────── day of week (0 - 6) (Sunday to Saturday; 7 is also Sunday on some systems)
     *    │ │ │ │ │                                       
     *    * * * * *
     */
    public class ScheduleTask : TimeTaskScheduler
    {
        private readonly ILogger<SpareTimeNudgeTask> Logger;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory, ILogger<SpareTimeNudgeTask> logger) : base(serviceScopeFactory)
        {
            this.Logger = logger;
        }

        protected override string Schedule => "*/5 * * * * *";

        public async override Task ScheduledTask(IServiceProvider serviceProvider)
        {
            this.Logger.LogInformation("Running task " + DateTime.UtcNow);


            var weatherService = serviceProvider.GetService<IWeatherService>();
            var nudgeService = serviceProvider.GetService<INudgeService>();
            var notificationService = serviceProvider.GetService<INotificationService>();

            var forecast = await weatherService.GetCurrentForecast();

            var pushNotificationService = serviceProvider.GetService<IPushNotificationService>();

            var userIds = serviceProvider.GetService<IUserService>().GetAllUserIds();

            var title = "Nudge of the day";
            var message = "Hello";
            foreach (var userId in userIds)
            {
                var nudgeId = nudgeService.AddNudge(userId, forecast);
                notificationService.Insert(message, nudgeId);
                pushNotificationService.PushToUser(userId, title, message);
            }

            // return Task.CompletedTask;
        }
    }
}
