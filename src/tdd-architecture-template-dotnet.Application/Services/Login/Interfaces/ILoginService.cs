using tdd_architecture_template_dotnet.Application.Models.Http;

namespace tdd_architecture_template_dotnet.Application.Services.Login.Interfaces
{
    public interface ILoginService
    {
        Task<Result<string>> GetLogin(string email);
    }
}
