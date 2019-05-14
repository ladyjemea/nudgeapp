namespace NudgeApp.Testing.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using NudgeApp.Common.Dtos;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.DataManagement.Implementation.Interfaces;
    using NudgeApp.Testing.Tests.Interfaces;

    public class DatabaseTesting : IDatabaseTesting
    {
        private readonly ILogger<DatabaseTesting> logger;
        private readonly INudgeOracleRepository anonymousNudgeOracleRepository;
        private readonly INudgeService nudgeService;
        private readonly IUserService userService;

        private readonly Random random;

        public DatabaseTesting(ILogger<DatabaseTesting> logger, INudgeOracleRepository anonymousNudgeOracleRepository, IUserService userService, INudgeService nudgeService)
        {
            this.logger = logger;
            this.anonymousNudgeOracleRepository = anonymousNudgeOracleRepository;
            this.userService = userService;
            this.nudgeService = nudgeService;

            this.random = new Random();
        }

        public void Run()
        {
            this.AddUsers(10);

            this.AddNudges(100);
        }

        private void AddNudges(int nudgeCount)
        {
            var userIds = this.userService.GetAllUserIds();

            for (int i = 0; i < nudgeCount; i++)
            {
                var forecast = new WeatherDto()
                {

                };

                var trip = new TripDto();

             //   this.nudgeService.AddNudge(userIds[random.Next(userIds.Count)], (TransportationType)(random.Next(3)), forecast, trip);
            }
        }

        private void AddUsers(int userCount)
        {
            for (int i = 0; i < userCount; i++)
                this.userService.CreateUser(random.Next().ToString(), random.Next().ToString(), random.Next().ToString(), random.Next().ToString(), (TransportationType)(random.Next(3)));
        }

        public void RunTestAnonymousDatabase()
        {
            var (durations, durationsAprox) = this.RunTests();

            var path = "file1.txt";
            var stream = new StreamWriter(path, true);

            var (count, sample_duration) = this.anonymousNudgeOracleRepository.Count();

            stream.WriteLine($"Reference, duration: {count}, {sample_duration}");
            this.ShowResults("Count distinct:", durations, stream);
            this.ShowResults("Count distinct approx:", durationsAprox, stream);
            stream.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------");
            stream.WriteLine();
            stream.Close();

            (durations, durationsAprox) = this.RunTests();

            path = "file2.txt";
            stream = new StreamWriter(path, true);

            stream.WriteLine($"Reference, duration: {count}, {sample_duration}");
            this.ShowResults("Count distinct:", durations, stream);
            this.ShowResults("Count distinct approx:", durationsAprox, stream);
            stream.WriteLine("------------------------------------------------------------------------------------------------------------------------------------------------------------");
            stream.WriteLine();
            stream.Close();
        }

        public void InsertRows(int entryCount, List<Guid> userIds = null)
        {
            Console.WriteLine("Inserting rows");
            var z = 0;
            for (var j = 0; j < 10; j++)
            {
                int i;
                for (i = z; i < entryCount / 10; i++)
                    this.anonymousNudgeOracleRepository.Insert(new Data.Entities.OracleNudgeEntity
                    {
                        Id = Guid.NewGuid(),
                        ActualTransportationType = (TransportationType)this.random.Next(4),
                        PrecipitationProbability = this.random.Next(101),
                        Result = (NudgeResult)this.random.Next(3),
                        RoadCondition = (RoadCondition)this.random.Next(4),
                        SkyCoverage = (SkyCoverageType)this.random.Next(3),
                        Temperature = (float)(this.random.Next(100) + this.random.NextDouble() - 50),
                        UserPreferedTransportationType = (TransportationType)this.random.Next(4),
                        Wind = (float)(this.random.Next(50) + this.random.NextDouble()),
                        UserId = userIds[this.random.Next(userIds.Count)]
                    });

                z = 0;
                Console.WriteLine($"Inserted {(j + 1) * i} rows");
            }

            Console.WriteLine("Insert finished");
        }

        private void ShowResults(string subject, List<long> results, StreamWriter stream = null)
        {
            double sum = 0;
            var mean = results.Average();

            results.ForEach(d => sum += Math.Pow(d - mean, 2));
            var deviation = Math.Sqrt(sum / results.Count);

            Console.WriteLine(subject);
            Console.WriteLine("Mean: " + results.Average());
            Console.WriteLine("Min: " + results.Min());
            Console.WriteLine("Max: " + results.Max());
            Console.WriteLine("Standard deviation: " + deviation);
            Console.WriteLine("Mean deviation min: " + (results.Average() - deviation));
            Console.WriteLine("Mean deviation max: " + (results.Average() + deviation));
            Console.WriteLine();
            Console.WriteLine();

            if (stream != null)
            {
                stream.WriteLine(subject);
                stream.WriteLine("Mean: " + results.Average());
                stream.WriteLine("Min: " + results.Min());
                stream.WriteLine("Max: " + results.Max());
                stream.WriteLine("Mean deviation: " + deviation);
                stream.WriteLine("Mean deviation min: " + (results.Average() - deviation));
                stream.WriteLine("Mean deviation max: " + (results.Average() + deviation));
                stream.WriteLine("Results: ");
                results.ForEach(r => stream.Write(r + " "));
                stream.WriteLine();
                stream.WriteLine();
            }
        }

        private (List<long> durations, List<long> durationsAprox) RunTests()
        {
            var durations = new List<long>();
            var durationsAprox = new List<long>();

            for (int i = 0; i < 10; i++)
            {
                var query = new QueryFilter
                {
                    ActualTransportationType = random.Next(100) % 2 == 0 ? (TransportationType?)random.Next(4) : null,
                    MaxPrecipitation = random.Next(100) % 2 == 0 ? (int?)(random.Next(50) + 50) : null,
                    MinPrecipitation = random.Next(100) % 2 == 0 ? (int?)random.Next(50) : null,
                    MaxTemperature = random.Next(100) % 2 == 0 ? (int?)random.Next(50) : null,
                    MinTemperature = random.Next(100) % 2 == 0 ? (int?)(random.Next(50) - 50) : null,
                    MaxWind = random.Next(100) % 2 == 0 ? (int?)(random.Next(25) + 25) : null,
                    MinWind = random.Next(100) % 2 == 0 ? (int?)random.Next(25) : null,
                    Result = random.Next(100) % 2 == 0 ? (NudgeResult?)random.Next(3) : null,
                    Road = random.Next(100) % 2 == 0 ? (RoadCondition?)random.Next(4) : null,
                    SkyCoverage = random.Next(100) % 2 == 0 ? (SkyCoverageType?)random.Next(3) : null,
                    UserPreferedTransportationType = random.Next(100) % 2 == 0 ? (TransportationType?)random.Next(4) : null
                };

                var (result, duration) = this.anonymousNudgeOracleRepository.CountDistinct(query);

                durations.Add(duration);

                var query_approx = new QueryFilter
                {
                    ActualTransportationType = random.Next(100) % 2 == 0 ? (TransportationType?)random.Next(4) : null,
                    MaxPrecipitation = random.Next(100) % 2 == 0 ? (int?)(random.Next(50) + 50) : null,
                    MinPrecipitation = random.Next(100) % 2 == 0 ? (int?)random.Next(50) : null,
                    MaxTemperature = random.Next(100) % 2 == 0 ? (int?)random.Next(50) : null,
                    MinTemperature = random.Next(100) % 2 == 0 ? (int?)(random.Next(50) - 50) : null,
                    MaxWind = random.Next(100) % 2 == 0 ? (int?)(random.Next(25) + 25) : null,
                    MinWind = random.Next(100) % 2 == 0 ? (int?)random.Next(25) : null,
                    Result = random.Next(100) % 2 == 0 ? (NudgeResult?)random.Next(3) : null,
                    Road = random.Next(100) % 2 == 0 ? (RoadCondition?)random.Next(4) : null,
                    SkyCoverage = random.Next(100) % 2 == 0 ? (SkyCoverageType?)random.Next(3) : null,
                    UserPreferedTransportationType = random.Next(100) % 2 == 0 ? (TransportationType?)random.Next(4) : null
                };

                var (result_approx, duration_approx) = this.anonymousNudgeOracleRepository.ApproxCount(query_approx);

                durationsAprox.Add(duration_approx);
            }

            return (durations, durationsAprox);
        }
    }
}
