namespace NudgeApp.DataAnalysis
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataAnalysis.API;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IAnalyzer, Analyzer>();
        }
    }
}
