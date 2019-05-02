namespace NudgeApp.Testing.Tests.Interfaces
{
    using System.Threading.Tasks;

    public interface IInMemoryStoreTesting
    {
        Task TestCachingSpeed();
        Task TestWeatherService();
    }
}