﻿namespace NudgeApp.DataManagement
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.DataManagement.ExternalApi.Bus;
    using NudgeApp.DataManagement.ExternalApi.Calendar;
    using NudgeApp.DataManagement.ExternalApi.Travel;
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
            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IDataAgregator, DataAgregator>();
            services.AddTransient<IPublicCalendarsService, PublicCalendarsService>();
            services.AddTransient<IWalkService, WalkService>();

            services.AddScoped<IMemoryCacheService, MemoryCacheService>();
            services.AddScoped<IPushNotificationService, PushNotificationService>();
        }
    }
}
