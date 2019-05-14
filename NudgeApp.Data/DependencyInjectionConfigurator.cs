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
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPreferencesRepository, PreferencesRepository>();
            services.AddScoped<IActualPreferencesRepository, ActualPreferencesRepository>();
            services.AddScoped<INudgeRepository, NudgeRepository>();
            services.AddScoped<INudgeOracleConnection, NudgeOracleConnection>();
            services.AddScoped<INudgeOracleRepository, NudgeOracleRepository>();
            services.AddScoped<ISubscritionRepository, SubscriptionRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<IAccountRepository, AccountRepository>();
        }
    }
}
