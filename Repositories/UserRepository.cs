using System.Collections.Generic;
using System.Linq;
using TodoBasicAPI.Models;

namespace TodoBasicAPI.Repositories
{
    public class UserRepository
    {
        public User Get(string username, string password)
        {
            var users = new List<User>();
            users.Add(new User { Id = 1, Username = "maria", Password = "123456", Role = "manager" });
            users.Add(new User { Id = 2, Username = "joao", Password = "654321", Role = "employee" });
            return users.Where(x => x.Username == username && x.Password == password).SingleOrDefault();
        }
    }
}