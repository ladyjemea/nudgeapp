namespace NudgeApp.DataAnalysis.ScheduledServices
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NudgeApp.DataAnalysis.ScheduledServices.TaskScheduler;
    using System;
    using System.Threading.Tasks;

    /* Example from:
     * https://thinkrethink.net/2018/05/31/run-scheduled-background-tasks-in-asp-net-core/
     * https://github.com/pgroene/ASPNETCoreScheduler
     *  ┌───────────── minute (0 - 59)
     *  │ ┌───────────── hour (0 - 23)
     *  │ │ ┌───────────── day of month (1 - 31)
     *  │ │ │ ┌───────────── month (1 - 12)
     *  │ │ │ │ ┌───────────── day of week (0 - 6) (Sunday to Saturday; 7 is also Sunday on some systems)
     *  │ │ │ │ │                                       
     *  * * * * *
     */
    public class ScheduleTask : ScheduledProcessor
    {
        private readonly ILogger<SpareTimeNudgeTask> Logger;

        public ScheduleTask(IServiceScopeFactory serviceScopeFactory, ILogger<SpareTimeNudgeTask> logger) : base(serviceScopeFactory)
        {
            this.Logger = logger;
        }

        protected override string Schedule => "* */1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            this.Logger.LogInformation("Running task " + DateTime.UtcNow);

            return Task.CompletedTask;
        }
    }
}
