using Newtonsoft.Json;
using NS.API.Data;
using NS.API.Models;
using NS.API.Utility;
using System.Collections.Generic;
using System.IO;
namespace NS.API.Helpers
{
    public class Seed
    {
        private readonly DataContext Context;

        public Seed(DataContext _context)
        {            
            Context = _context;
        }

        public void SeederUser()
        {
            var userJson = File.ReadAllText("JsonData/UserData.json");
            var userData = JsonConvert.DeserializeObject<List<User>>(userJson);

            foreach (var user in userData)
            {
                byte [] PasswordHash, PasswordSalt = null;
                PasswordUtility.CreatePasswordHash_Salt("Password",out PasswordHash,out PasswordSalt);

                user.PasswordHash = PasswordHash;
                user.PasswordSalt = PasswordSalt;
                user.Username = user.Username.ToLower();

                Context.Add(user);
            }    
            Context.SaveChanges();
        }
    }
}