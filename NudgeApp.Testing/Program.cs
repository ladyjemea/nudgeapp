﻿
namespace NudgeApp.Testing
{
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
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

            var userIds = new List<Guid>();

            for (int i = 0; i < 5; i++)
            {
                for (var j = 0; j < 2000; j++)
                    userIds.Add(Guid.NewGuid());

                Console.WriteLine("Prepared users");

                Console.WriteLine($"Round {i + 1}");

                testService.InsertRows(userIds);

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

            services.AddTransient<IDatabaseTesting, DatabaseTesting>();
        }
    }
}
