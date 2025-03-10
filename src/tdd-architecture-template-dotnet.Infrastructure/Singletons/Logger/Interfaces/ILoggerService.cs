namespace tdd_architecture_template_dotnet.Infrastructure.Singletons.Logger.Interfaces
{
    public interface ILoggerService
    {
        void LogInfo(string message);
        void LogError(string message);
    }
}
