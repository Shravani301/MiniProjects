using ContactApp.Models;
using ContactApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Controller
{
    internal class UserController
    {
        public void AddUser(int userId, string firstName, string lastName, bool isActive, bool isAdmin)
        {
            var user = new User
            {
                UserId = userId,
                FirstName = firstName,
                LastName = lastName,
                IsActive = isActive,
                IsAdmin = isAdmin
            };
            UserService.AddUser(user);
        }

        public void ModifyUser(int userId, Action<User> modifyAction)
        {
            UserService.ModifyUser(userId, modifyAction);
        }

        public void DeleteUser(int userId)
        {
            UserService.DeleteUser(userId);
        }

        public IEnumerable<User> GetAllUsers()
        {
            return UserService.GetAllUsers();
        }

        public User GetUserById(int userId)
        {
            return UserService.GetUserById(userId);
        }

        public bool IsAdmin(int userId)
        {
            return UserService.IsAdmin(userId);
        }
    }
}
