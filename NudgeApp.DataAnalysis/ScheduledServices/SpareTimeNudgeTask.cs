namespace NudgeApp.DataAnalysis.ScheduledServices
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataAnalysis.ScheduledServices.TaskScheduler;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using System;
    using System.Threading.Tasks;

    public class SpareTimeNudgeTask : ScheduledProcessor
    {
        public SpareTimeNudgeTask(IServiceScopeFactory serviceScopeFactory) : base(serviceScopeFactory) { }

        protected override string Schedule => "*/1 * * * *";

        public override Task ProcessInScope(IServiceProvider serviceProvider)
        {
            var weatherService = serviceProvider.GetService<IWeatherService>();

            return Task.CompletedTask;
        }

    }

   
}
