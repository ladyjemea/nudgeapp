namespace NudgeApp.DataManagement
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Implementation;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IWeatherApi, WeatherApi>();
            services.AddSingleton<IBusService, BusService>();
            services.AddSingleton<IUserLogic, UserLogic>();
            services.AddSingleton<INudgeLogic, NudgeLogic>();
            services.AddSingleton<IPushNotificationLogic, PushNotificationLogic>();                        
        }
    }
}
