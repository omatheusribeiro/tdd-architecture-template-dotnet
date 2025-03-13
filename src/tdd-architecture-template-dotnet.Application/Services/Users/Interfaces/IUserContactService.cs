using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;

namespace tdd_architecture_template_dotnet.Application.Services.Users.Interfaces
{
    public interface IUserContactService
    {
        Task<Result<UserContactViewModel>> Put(UserContactViewModel address);
    }
}
