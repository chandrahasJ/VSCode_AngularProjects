using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NS.API.Models;

namespace NS.API.Data
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext Context;

        public DatingRepository(DataContext _context)
        {
            Context = _context;
        }
        public void Add<T>(T entity) where T : class
        {
            Context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            Context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var User = await Context.Users.Include(p => p.Photos).FirstOrDefaultAsync(pp => pp.Id == id);

            return User;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var Users = await Context.Users.Include(p => p.Photos).ToListAsync();

            return Users;
        }

        public async Task<bool> SaveAll()
        {
            return await Context.SaveChangesAsync() > 0;
        }
    }
}