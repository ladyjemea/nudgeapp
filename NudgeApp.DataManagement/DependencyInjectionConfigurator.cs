namespace NudgeApp.DataManagement
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Weather;
    using NudgeApp.DataManagement.UserControl;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IWeatherApi, WeatherApi>();
            services.AddSingleton<ITripSearch, TripSearch>();
            services.AddSingleton<IUserLogic, UserLogic>();
        }
    }
}
