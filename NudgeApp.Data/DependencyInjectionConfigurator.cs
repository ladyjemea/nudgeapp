namespace NudgeApp.Data
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.Data.OracleDb;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.Data.Repositories;
    using NudgeApp.Data.Repositories.Interfaces;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPreferencesRepository, PreferencesRepository>();
            services.AddSingleton<INudgeRepository, NudgeRepository>();
            services.AddSingleton<INudgeOracleConnection, NudgeOracleConnection>();
            services.AddSingleton<INudgeOracleRepository, NudgeOracleRepository>();
            services.AddSingleton<IPushNotificationRepository, PushNotificationRepository>();
            services.AddSingleton<INotificationRepository, NotificationRepository>();
            services.AddSingleton<IAnonymousNudgeRepository, AnonymousNudgeRepository>();
        }
    }
}
