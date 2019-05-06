namespace NudgeApp.Testing.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using NudgeApp.Common.Enums;
    using NudgeApp.Data.OracleDb.Queries;
    using NudgeApp.Testing.Tests.Interfaces;

    public class DatabaseTesting : IDatabaseTesting
    {
        private readonly ILogger<DatabaseTesting> logger;
        private readonly INudgeOracleRepository anonymousNudgeOracleRepository;

        private readonly Random random;
        /*private readonly IList<Guid> ids = new List<Guid>
        {
            Guid.Parse("55dc9ccc-a660-40ad-aa5c-98183b083efd"),
            Guid.Parse("219c63bc-3e0c-4111-8eeb-e8870a83fedd"),
            Guid.Parse("4c09e73a-2e87-4e6f-8d46-cc7d0d3e6c2a"),
            Guid.Parse("5b977f72-741c-4988-97eb-eeaac44624bf"),
            Guid.Parse("935c42e9-e805-43ea-a2fc-5ec7fd0319a2"),
            Guid.Parse("9e916f93-884f-49fb-8a02-02b7ad35b2a1"),
            Guid.Parse("b082f62f-4365-4a71-a7d9-a267b47bfd87"),
            Guid.Parse("1a3a70ec-7ad2-4d0b-9eaa-5b9693ee0de3"),
            Guid.Parse("5eb2e018-8484-4f8a-a81c-09508337ef94"),
            Guid.Parse("e963330b-8468-43ae-bf46-ec4b3b82bbc9"),
            Guid.Parse("b2885af5-35cf-404c-a649-e1d27a2b2ece"),
            Guid.Parse("9e69206e-eb88-4509-b25d-edc7095ed640"),
            Guid.Parse("705579e1-c452-4fb5-81d4-d7a30a12d791"),
            Guid.Parse("e388203a-8da0-4e8d-b4d4-6e5f5f52b329"),
            Guid.Parse("0bffb16c-330a-4b49-85d9-22cfad54efb8"),
            Guid.Parse("106bdb90-3868-417f-b76f-f75d244666bf"),
            Guid.Parse("8449ac03-c66b-4780-ad40-bcf76f5a4921"),
            Guid.Parse("c628f6e4-ffd1-4f10-9216-d1772f51c0c9"),
            Guid.Parse("73d7cc5f-88a3-4e7e-b38b-ed767609d1c7"),
            Guid.Parse("5ab1f384-1f56-4968-a354-e1024194eb36"),
            Guid.Parse("fef5549d-2752-4088-b4a0-df412e6af1ba"),
            Guid.Parse("196fdefb-e96c-4947-b9b8-56502ffdb155"),
            Guid.Parse("6d97278d-a893-46b7-aba6-3feaa2816043"),
            Guid.Parse("a5f5fd27-4f1f-4334-a71b-5c024b814a3f"),
            Guid.Parse("24b48ce1-d854-41b6-8d14-29acbbec1502"),
            Guid.Parse("7c5a12f7-1fd3-45e4-9302-f71951ca7981"),
            Guid.Parse("c114062d-6ad3-4dab-8959-6c120b659110"),
            Guid.Parse("4621a163-d289-40b5-a904-229bcd58b45b"),
            Guid.Parse("050a03ee-e5fb-479b-bdeb-936c060c62d7"),
            Guid.Parse("346a733d-697c-4983-bccd-41ca8778e18f")
        };
        */
        public DatabaseTesting(ILogger<DatabaseTesting> logger, INudgeOracleRepository anonymousNudgeOracleRepository)
        {
            this.logger = logger;
            this.anonymousNudgeOracleRepository = anonymousNudgeOracleRepository;

            this.random = new Random();
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

        public void InsertRows(List<Guid> userIds = null)
        {
            Console.WriteLine("Inserting rows");
            var z = 0;
            for ( var j = 0; j < 10; j++)
            {
                int i;
                for (i = z; i < 200000; i++)
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
                    Result_success = random.Next(100) % 2 == 0 ? (NudgeResult?)random.Next(3) : null,
                    Road = random.Next(100) % 2 == 0 ? (RoadStateType?)random.Next(4) : null,
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
                    Result_success = random.Next(100) % 2 == 0 ? (NudgeResult?)random.Next(3) : null,
                    Road = random.Next(100) % 2 == 0 ? (RoadStateType?)random.Next(4) : null,
                    SkyCoverage = random.Next(100) % 2 == 0 ? (SkyCoverageType?)random.Next(3) : null,
                    UserPreferedTransportationType = random.Next(100) % 2 == 0 ? (TransportationType?)random.Next(4) : null
                };

                //var (result_approx, duration_approx) = this.anonymousNudgeOracleRepository.ApproxCount(query_approx);

                 //durationsAprox.Add(duration_approx);
            }

            return (durations, durationsAprox);
        }
    }
}
