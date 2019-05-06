namespace NudgeApp.DataManagement
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Calendar;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Helpers;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddTransient<IWeatherService, WeatherService>();
            services.AddTransient<IBusService, BusService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<INudgeService, NudgeService>();
            services.AddTransient<IPushNotificationLogic, PushNotificationLogic>();
            services.AddTransient<IAnalysisConversion, AnalysisConversion>();
            services.AddTransient<IPublicCalendarsService, PublicCalendarsService>();

            services.AddSingleton<IMemoryCacheService, MemoryCacheService>();
        }
    }
}
