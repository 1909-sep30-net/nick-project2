using System;
using System.Threading.Tasks;
using KitchenRestService.Logic;
using Microsoft.EntityFrameworkCore;

namespace KitchenRestService.Data
{
    public class UserRepo : IUserRepo
    {
        private readonly KitchenContext _context;

        public UserRepo(KitchenContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User> CreateUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }
    }
}
