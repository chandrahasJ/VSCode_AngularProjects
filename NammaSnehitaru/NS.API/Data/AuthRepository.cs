using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NS.API.Models;
using NS.API.Utility;

namespace NS.API.Data
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;

        }
        public async Task<bool> IsUserExists(string Username)
        {
            var isUserExists = await _context.Users.AnyAsync(x => x.Username == Username);
            
            return isUserExists;
        }

        public async Task<User> Login(string Username, string Password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == Username);

            if(user == null) return null;

            if(!PasswordUtility.VerifyPassword(Password,user.PasswordHash,user.PasswordSalt)) return null;

            return user;
        }

        public async Task<User> Register(User user, string Password)
        {
            byte[] PasswordHash, PasswordSalt;
            PasswordUtility.CreatePasswordHash_Salt(Password, out PasswordHash, out PasswordSalt);

            user.PasswordHash = PasswordHash;
            user.PasswordSalt = PasswordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

       
    }
}