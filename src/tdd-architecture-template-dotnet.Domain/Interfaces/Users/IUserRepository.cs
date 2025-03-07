using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Domain.Interfaces.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(int id);
        Task<User> Put(User user);
        Task<User> Post(User user);
        Task<User> Delete(User user);
    }
}
