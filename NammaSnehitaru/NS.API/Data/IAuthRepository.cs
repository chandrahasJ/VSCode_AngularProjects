using System.Threading.Tasks;
using NS.API.Models;

namespace NS.API.Data
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string Password);
         Task<User> Login(string Username,string Password);
         Task<bool> IsUserExists(string Username);
    }
}