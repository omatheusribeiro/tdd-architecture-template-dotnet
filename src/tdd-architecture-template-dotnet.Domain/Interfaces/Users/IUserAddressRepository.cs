using tdd_architecture_template_dotnet.Domain.Entities.Users;

namespace tdd_architecture_template_dotnet.Domain.Interfaces.Users
{
    public interface IUserAddressRepository
    {
        Task<IEnumerable<UserAddress>> GetAll();
        Task<UserAddress> GetById(int id);
        Task<UserAddress> Put(UserAddress address);
        Task<UserAddress> Post(UserAddress address);
        Task<UserAddress> Delete(UserAddress address);
    }
}
