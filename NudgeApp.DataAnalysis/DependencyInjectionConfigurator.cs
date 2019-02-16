namespace NudgeApp.DataAnalysis
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataAnalysis.API;
    using NudgeApp.DataAnalysis.Implementation;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IAnalyzer, Analyzer>();
            services.AddSingleton<IPushNotificationService, PushNotificationService>();
        }
    }
}
