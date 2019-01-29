namespace NudgeApp.Data
{
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.Data.Repositories.User;

    public static class DependencyInjectionConfigurator
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IPreferencesRepository, PreferencesRepository>();
        }
    }
}
