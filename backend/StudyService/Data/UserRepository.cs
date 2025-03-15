// namespace StudyService.Data
// {
//     public class UserRepository
//     {
//         private static List<Models.User> _users = new List<Models.User> {};

//         public List<Models.User> GetAllUsers() => _users;

//         public Models.User GetUserByEmail(string email) => _users.FirstOrDefault(u => u.Email == email);

//         public void AddUser(Models.User user)
//         {
//             user.Id = _users.Count + 1; // Простая генерация ID
//             _users.Add(user);
//         }
//     }
// }
using StudyService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StudyService.Data
{
    public class UserRepository
    {
        private readonly StudyServiceDbContext _context;

        public UserRepository(StudyServiceDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUserByEmail(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public bool UserExistsById(int id)
        {
            return _context.Users.Any(u => u.Id == id);
        }

        public bool UserExistsByEmail(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}