using Microsoft.EntityFrameworkCore;
using tdd_architecture_template_dotnet.Domain.Entities.Users;
using tdd_architecture_template_dotnet.Domain.Interfaces.Users;
using tdd_architecture_template_dotnet.Infrastructure.Data.Context;

namespace tdd_architecture_template_dotnet.Infrastructure.Repositories.Users
{
    public class UserContactRepository : IUserContactRepository
    {
        private ApplicationDbContext _context;

        public UserContactRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserContact>> GetAll()
        {
            return await _context.UserContacts.ToListAsync();
        }

        public async Task<UserContact> GetById(int id)
        {
            return await _context.UserContacts.AsNoTracking().Where(u => u.Id == id).FirstOrDefaultAsync();
        }

        public async Task<UserContact> GetByEmail(string email)
        {
            return await _context.UserContacts.AsNoTracking().Where(u => u.Email == email).FirstAsync();
        }

        public async Task<UserContact> Put(UserContact contact)
        {
            contact.ChangeDate = DateTime.UtcNow;

            _context.UserContacts.Update(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<UserContact> Post(UserContact contact)
        {
            contact.CreationDate = DateTime.UtcNow;

            _context.UserContacts.Add(contact);
            await _context.SaveChangesAsync();

            return contact;
        }

        public async Task<UserContact> Delete(UserContact contact)
        {
            _context.UserContacts.Remove(contact);
            await _context.SaveChangesAsync();

            return contact;
        }
    }
}
