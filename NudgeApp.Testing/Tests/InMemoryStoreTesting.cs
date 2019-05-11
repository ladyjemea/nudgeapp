namespace NudgeApp.Testing.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Threading.Tasks;
    using NudgeApp.DataManagement.ExternalApi.Weather.Interfaces;
    using NudgeApp.DataManagement.Helpers;
    using NudgeApp.Testing.Tests.Interfaces;

    public class InMemoryStoreTesting : IInMemoryStoreTesting
    {
        private readonly IMemoryCacheService MemoryCacheService;
        private readonly IWeatherService WeatherService;

        private readonly Random randomGenerator;
        private StreamWriter stream;

        public InMemoryStoreTesting(IMemoryCacheService memoryCacheService, IWeatherService weatherService)
        {
            this.MemoryCacheService = memoryCacheService;
            this.WeatherService = weatherService;
            this.randomGenerator = new Random();

            this.stream = new StreamWriter("in memory tests.txt", true);
        }
        public async Task TestWeatherService() { }
        /*
        public async Task TestWeatherService()
        {
            var watchT = Stopwatch.StartNew();
            await this.WeatherService.GetCurrentForecast();
            watchT.Stop();

            Console.WriteLine($"Weather forecast: { watchT.ElapsedMilliseconds}");

            long onlineSaveTime = 0;
            for (var i = 0; i < 10; i++)
            {
                var watch = Stopwatch.StartNew();

                await this.WeatherService.GetCurrentForecastRandom();

                watch.Stop();
                onlineSaveTime += watch.ElapsedMilliseconds;
            }

            Console.WriteLine($"Weather forecast online: {onlineSaveTime}");
            Console.WriteLine($"Weather forecast online average: {(double)(onlineSaveTime/10)}");

            long noMemoryTime = 0;
            for (var i = 0; i < 10; i++)
            {
                var watch = Stopwatch.StartNew();

                await this.WeatherService.GetCurrentForecastNoMemory();

                watch.Stop();
                noMemoryTime += watch.ElapsedMilliseconds;
            }

            Console.WriteLine($"Weather forecast no memory: {noMemoryTime}");
            Console.WriteLine($"Weather forecast no memory average: {(double)(noMemoryTime / 10)}");
            

            long offlineSaveTime = 0;
            for (var i = 0; i < 10; i++)
            {
                var watch = Stopwatch.StartNew();

                await this.WeatherService.GetCurrentForecast();

                watch.Stop();
                offlineSaveTime += watch.ElapsedMilliseconds;
            }

            Console.WriteLine($"Weather forecast offline: {offlineSaveTime}");
            Console.WriteLine($"Weather forecast offline average: {(double)(offlineSaveTime / 10)}");

        }
        */
        public async Task TestCachingSpeed()
        {
            var objects = new List<TestObject>();

            Console.WriteLine("Starting tests");

            int i, j, z;
            for (j = 0; j < 10; j++)
            {
                Write();

                long saveTime = 0;
                for (i = 0; i < 10000; i++)
                {
                    var obj = new TestObject
                    {
                        Key = Guid.NewGuid().ToString(),
                        Data1 = this.randomGenerator.Next(),
                        Data2 = this.randomGenerator.Next()
                    };
                    objects.Add(obj);

                    var watch = Stopwatch.StartNew();

                    await this.MemoryCacheService.SaveAsync(obj.Key, obj);

                    watch.Stop();

                    saveTime += watch.ElapsedMilliseconds;
                }

                Write($"{(j + 1) * i} entries");
                Write($"Total save time: {saveTime}");
                Write($"Average save time: {(double)(saveTime / 10000)}");

                long readTime = 0;
                for (z = 0; z < 10; z++)
                {
                    var expected = objects[randomGenerator.Next(objects.Count)];

                    var watch = Stopwatch.StartNew();

                    var actual = await this.MemoryCacheService.GetAsync<TestObject>(expected.Key);
                    
                    watch.Stop();

                    if (expected.Key != actual.Key)
                    {
                        throw new Exception("Not expected object.");
                    }

                    readTime += watch.ElapsedMilliseconds;
                }

                Write($"Total read time: {readTime}");
                Write($"Average read time: {(double)(readTime / 10)}");
            }

            stream.Close();
        }

        private void Write(string text = "")
        {
            this.stream.WriteLine(text);
            Console.WriteLine(text);
        }

        [Serializable]
        private class TestObject
        {
            public string Key { get; set; }
            public int Data1 { get; set; }
            public int Data2 { get; set; }
        }
    }
}
