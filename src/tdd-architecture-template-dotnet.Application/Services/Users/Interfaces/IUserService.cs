using tdd_architecture_template_dotnet.Application.Models.Http;
using tdd_architecture_template_dotnet.Application.ViewModels.Users;

namespace tdd_architecture_template_dotnet.Application.Services.Users.Interfaces
{
    public interface IUserService
    {
        Task<Result<IEnumerable<UserViewModel>>> GetAll();
        Task<Result<UserViewModel>> GetById(int id);
        Task<Result<UserViewModel>> Put(UserViewModel user);
        Task<Result<UserViewModel>> Post(UserViewModel user);
        Task<Result<UserViewModel>> Delete(UserViewModel user);
    }
}
