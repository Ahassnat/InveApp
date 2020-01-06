using System.Threading.Tasks;
using Test.API.Models;

namespace Test.API.Data
{
    public interface IAuthRepo
    {
        Task<User> Register (User user, string password);
        Task<User> Login (string username, string password);
        Task<bool> UserExists(string username);
    }
}