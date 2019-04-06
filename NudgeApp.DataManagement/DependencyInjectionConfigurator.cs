namespace NudgeApp.DataManagement
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Helpers;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IWeatherService, WeatherService>();
            services.AddSingleton<IBusService, BusService>();
            services.AddSingleton<IUserService, UserService>();
            services.AddSingleton<INudgeService, NudgeService>();
            services.AddSingleton<IPushNotificationLogic, PushNotificationLogic>();                        
            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();                        
            services.AddSingleton<IAnalysisConversion, AnalysisConversion>();                        
        }
    }
}
