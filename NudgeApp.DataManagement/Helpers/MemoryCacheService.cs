namespace NudgeApp.DataManagement.Helpers
{
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Caching.Distributed;
    using Microsoft.Extensions.Logging;
    using NudgeApp.DataManagement.Implementation.Interfaces;

    public class MemoryCacheService : IMemoryCacheService
    {
        private readonly IDistributedCache DistributedCache;
        private readonly ILogger Logger;

        public MemoryCacheService(IDistributedCache distributedCache, ILogger<MemoryCacheService> logger)
        {
            this.DistributedCache = distributedCache;
            this.Logger = logger;
        }

        public async Task SaveAsync(string key, object value, TimeSpan? expires = null)
        {
            this.Logger.LogInformation("Saving to redis cache.");

            var token = new CancellationToken();

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream())
            {
                bf.Serialize(ms, value);

                if (expires == null)
                    expires = new TimeSpan(1, 0, 0);

                var options = new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = expires };
                await this.DistributedCache.SetAsync(key, ms.ToArray(), options, token);
            }
        }

        public async Task<TObject> GetAsync<TObject>(string key)
        {
            this.Logger.LogInformation("Fecthing from redis cache.");

            var token = new CancellationToken();

            var data = await this.DistributedCache.GetAsync(key, token);

            if (data == null)
                return default(TObject);

            BinaryFormatter bf = new BinaryFormatter();
            using (MemoryStream ms = new MemoryStream(data))
            {
                object obj = bf.Deserialize(ms);
                return (TObject)obj;
            }
        }
    }
}
