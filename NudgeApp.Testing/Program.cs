
namespace NudgeApp.Testing
{
    using System;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using NudgeApp.Testing.Tests;
    using NudgeApp.Testing.Tests.Interfaces;

    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                            .AddLogging();

            ConfigureDependencyInjection(services);

            var serviceProvider = services.BuildServiceProvider();

            var testService = serviceProvider.GetService<IDatabaseTesting>();
             
            testService.InsertRows();   

            //testService.RunTestAnonymousDatabase();
        }



        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            NudgeApp.Data.DependencyInjectionConfigurator.Configure(services);
            NudgeApp.DataManagement.DependencyInjectionConfigurator.Configure(services);
            NudgeApp.DataAnalysis.DependencyInjectionConfigurator.Configure(services);

            services.AddTransient<IDatabaseTesting, DatabaseTesting>();
        }
    }
}
