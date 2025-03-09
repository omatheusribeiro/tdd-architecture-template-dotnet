using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;

namespace tdd_architecture_template_dotnet.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _context.Users.Include(a => a.Address).Include(c => c.Contact).ToListAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _context.Users.AsNoTracking().Include(a => a.Address).Include(c => c.Contact).Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> Put(User user)
        {
            user.ChangeDate = DateTime.UtcNow;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Post(User user)
        {
            user.CreationDate = DateTime.UtcNow;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User> Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}
