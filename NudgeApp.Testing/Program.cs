
namespace NudgeApp.Testing
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using NudgeApp.Testing.Tests;
    using NudgeApp.Testing.Tests.Interfaces;

    class Program
    {
        static void Main(string[] args)
        {
            var services = new ServiceCollection()
                            .AddLogging();

            ConfigureDependencyInjection(services);

            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = "localhost";
                options.InstanceName = "TestAppInstance";
            });

            var serviceProvider = services.BuildServiceProvider();

            RunDatabaseTesting(serviceProvider.GetService<IDatabaseTesting>());
            //RunInMemoryTesting(serviceProvider.GetService<IInMemoryStoreTesting>());
        }

        private static void RunInMemoryTesting(IInMemoryStoreTesting testService)
        {
            //testService.TestCachingSpeed().Wait();
            testService.TestWeatherService().Wait();
        }

        private static void RunDatabaseTesting(IDatabaseTesting testService)
        {
            var userIds = new List<Guid>();

            Console.WriteLine("huge test!!!");
            
            for (int i = 0; i < 5; i++)
            {
                for (var j = 0; j < 2000; j++)
                    userIds.Add(Guid.NewGuid());

                Console.WriteLine("Prepared users");

                Console.WriteLine($"Round {i + 1}");

                testService.InsertRows(20000000, userIds);

                Console.WriteLine("Starting test 1");
                testService.RunTestAnonymousDatabase();
                Console.WriteLine("Starting test 2");
                testService.RunTestAnonymousDatabase();

                Console.WriteLine("");
                Console.WriteLine("------------------------------------------------------------");
            }

        }

        private static void ConfigureDependencyInjection(IServiceCollection services)
        {
            NudgeApp.Data.DependencyInjectionConfigurator.Configure(services);
            NudgeApp.DataManagement.DependencyInjectionConfigurator.Configure(services);

            services.AddTransient<IDatabaseTesting, DatabaseTesting>();
            services.AddTransient<IInMemoryStoreTesting, InMemoryStoreTesting>();
        }
    }
}
