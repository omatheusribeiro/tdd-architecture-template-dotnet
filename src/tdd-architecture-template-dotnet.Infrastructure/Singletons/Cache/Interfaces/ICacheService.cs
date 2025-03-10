namespace tdd_architecture_template_dotnet.Infrastructure.Singletons.Cache.Interfaces
{
    public interface ICacheService
    {
        T? Get<T>(string key);
        void Set<T>(string key, T value, TimeSpan expiration);
    }
}
