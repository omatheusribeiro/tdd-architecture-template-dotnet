using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;

namespace tdd_architecture_template_dotnet.Infrastructure.Repositories.Users
{
    public class UserAddresRepository : IUserAddressRepository
    {
        private ApplicationDbContext _context;

        public UserAddresRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserAddress>> GetAll()
        {
            return await _context.UserAddresses.AsTracking().ToListAsync();
        }

        public async Task<UserAddress> GetById(int id)
        {
            return await _context.UserAddresses.AsNoTracking().Where(u => u.Id == id).AsTracking().FirstOrDefaultAsync();
        }

        public async Task<UserAddress> Put(UserAddress address)
        {
            address.ChangeDate = DateTime.UtcNow;

            _context.UserAddresses.Update(address);
            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<UserAddress> Post(UserAddress address)
        {
            address.CreationDate = DateTime.UtcNow;

            _context.UserAddresses.Add(address);
            await _context.SaveChangesAsync();

            return address;
        }

        public async Task<UserAddress> Delete(UserAddress address)
        {
            _context.UserAddresses.Remove(address);
            await _context.SaveChangesAsync();

            return address;
        }
    }
}
