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
            services.AddScoped<IAnalyzer, Analyzer>();
            services.AddTransient<IHostedService, SpareTimeNudgeTask>();
           // services.AddTransient<IHostedService, ScheduleTask>();
        }
    }
}
