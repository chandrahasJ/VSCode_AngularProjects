using System.Collections.Generic;
using System.Threading.Tasks;
using NS.API.Models;

namespace NS.API.Data
{
    public interface IDatingRepository
    {
         void Add<T>(T entity) where T : class;

         void Delete<T>(T entity) where T : class;

         Task<bool> SaveAll();

         Task<User> GetUser(int id);

         Task<IEnumerable<User>> GetUsers();
    }
}