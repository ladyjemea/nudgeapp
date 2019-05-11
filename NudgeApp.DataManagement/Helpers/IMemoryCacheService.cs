namespace NudgeApp.DataManagement.Helpers
{
    using System;
    using System.Threading.Tasks;

    public interface IMemoryCacheService
    {
        Task SaveAsync(string key, object value, TimeSpan? expires = null);
        Task<TObject> GetAsync<TObject>(string key);
    }
}