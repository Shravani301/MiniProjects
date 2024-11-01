using ContactApp.Exceptions;
using ContactApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Services
{
    internal static class UserService
    {
        private static List<User> users = new List<User>
        {
            new User
            {
                UserId = 1,
                FirstName = "Shravani",
                LastName = "Konga",
                IsAdmin = true,
                IsActive = true,
                Contacts = new List<Contact>()
            },
            new User
            {
                UserId = 2,
                FirstName = "Shirisha",
                LastName = "Konga",
                IsAdmin = false,
                IsActive = true,
                Contacts = new List<Contact>
                {
                    new Contact
                    {
                        ContactId = 1,
                        FirstName = "Chandana",
                        LastName = "Konga",
                        IsActive = true,
                        ContactDetails = new List<ContactDetails>
                        {
                            new ContactDetails { ContactDetailsId = 1, Type = "Mobile", Number = "1234567890" }
                        }
                    }
                }
            }
        };
        public static User GetUserById(int id)
        {
            return users.FirstOrDefault(u => u.UserId == id) ??
                   throw new UserNotFoundException("User not found.");
        }

        public static void AddUser(User user)
        {
            users.Add(user);
        }

        public static void ModifyUser(int userId, Action<User> modifyAction)
        {
            var user = GetUserById(userId);
            modifyAction(user);
        }

        public static void DeleteUser(int userId)
        {
            ModifyUser(userId, u => u.IsActive = false); 
        }

        public static List<User> GetAllUsers()
        {
            List<User> userList = new List<User>(users);
            
            return userList;
        }
        public static bool IsAdmin(int userId)
        {
            return GetUserById(userId).IsAdmin;
        }
    }
}

