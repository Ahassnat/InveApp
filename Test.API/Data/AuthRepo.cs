using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Test.API.Models;

namespace Test.API.Data
{
    public class AuthRepo : IAuthRepo
    {
        private readonly DataContext _context;
        public AuthRepo(DataContext context)
        {
            _context = context;

        }
        public async Task<User> Login(string username, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserName == username);
            if (user == null) return null;
            return user;
        }

        public async Task<User> Register(User user, string password)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> UserExists(string username)
        {
             if(await _context.Users.AnyAsync(x=>x.UserName==username))return true;
            return false;
        }
    }
}