namespace NudgeApp.DataAnalysis
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using NudgeApp.DataAnalysis.API;
    using NudgeApp.DataAnalysis.Implementation;
    using NudgeApp.DataAnalysis.ScheduledServices;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IAnalyzer, Analyzer>();
            services.AddSingleton<IPushNotificationService, PushNotificationService>();

            services.AddSingleton<IHostedService, SpareTimeNudgeTask>();
        }
    }
}
