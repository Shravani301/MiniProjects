using ContactApp.Controller;
using ContactApp.Exceptions;
using System;

namespace ContactApp.Presentation
{
    public class AdminMenu
    {
        private static UserController userController = new UserController();

        public static void DisplayAdminMenu()
        {
            bool exitMenu = false;

            while (!exitMenu)
            {
                Console.WriteLine("\nAdmin Menu:\n" +
                    "1. Add new User\n" +
                    "2. Modify existing User\n" +
                    "3. Delete User (soft)\n" +
                    "4. Display all Users\n" +
                    "5. Find User\n" +
                    "6. Logout\n" +
                    "Choose an option:");

                string choice = Console.ReadLine();
                exitMenu = AdminFunctionality(choice);
            }
        }

        private static bool AdminFunctionality(string choice)
        {
            switch (choice)
            {
                case "1":
                            AddUser();
                            break;
                case "2":
                            ModifyUser();
                            break;
                case "3":
                            DeleteUser();
                            break;
                case "4":
                            DisplayAllUsers();
                            break;
                case "5":
                            FindUser();
                            break;
                case "6":
                            Console.WriteLine("Logging out...");
                            Menu.DisplayMainMenu();
                            return true;
                default:
                            Console.WriteLine("Invalid option. Please choose a valid option.");
                            break;
            }
            return false;
        }

        private static void AddUser()
        {
            try
            {
                Console.Write("Enter User Id: ");
                int userId = Convert.ToInt32(Console.ReadLine());
                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Is User Active (true/false): ");
                bool isActive = Convert.ToBoolean(Console.ReadLine());
                Console.Write("Is User Admin (true/false): ");
                bool isAdmin = Convert.ToBoolean(Console.ReadLine());

                userController.AddUser(userId, firstName, lastName, isActive, isAdmin);
                Console.WriteLine("User added successfully.");
            }
            catch (InvalidUserActionException e)
            {
                Console.WriteLine("Invalid input format. Please enter the correct data type.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding user: {ex.Message}");
            }
        }

        private static void ModifyUser()
        {
            try
            {
                Console.Write("Enter User Id to modify: ");
                int userId = Convert.ToInt32(Console.ReadLine());

                Console.Write("Enter new First Name (or press Enter to skip): ");
                string firstName = Console.ReadLine();
                Console.Write("Enter new Last Name (or press Enter to skip): ");
                string lastName = Console.ReadLine();
                Console.Write("Set Active Status (true/false) (or press Enter to skip): ");
                string activeInput = Console.ReadLine();
                bool? isActive = string.IsNullOrEmpty(activeInput) ? null : Convert.ToBoolean(activeInput);

                userController.ModifyUser(userId, user =>
                {
                    if (!string.IsNullOrEmpty(firstName)) user.FirstName = firstName;
                    if (!string.IsNullOrEmpty(lastName)) user.LastName = lastName;
                    if (isActive.HasValue) user.IsActive = isActive.Value;
                });
                Console.WriteLine("User modified successfully.");
            }
            catch (InvalidUserActionException e)
            {
                Console.WriteLine("Invalid input format. Please enter the correct data type.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while modifying user: {ex.Message}");
            }
        }

        private static void DeleteUser()
        {
            try
            {
                Console.Write("Enter User Id to delete (soft delete): ");
                int userId = Convert.ToInt32(Console.ReadLine());
                userController.DeleteUser(userId);
                Console.WriteLine("User deleted successfully (soft delete).");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while deleting user: {ex.Message}");
            }
        }

        private static void DisplayAllUsers()
        {
            try
            {
                var users = userController.GetAllUsers();
                Console.WriteLine("All Users:");
                foreach (var user in users)
                {
                    Console.WriteLine($"ID: {user.UserId}, Name: {user.FirstName} {user.LastName}, Active: {user.IsActive}, Admin: {user.IsAdmin}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while displaying users: {ex.Message}");
            }
        }

        private static void FindUser()
        {
            try
            {
                Console.Write("Enter User Id to find: ");
                int userId = Convert.ToInt32(Console.ReadLine());
                var user = userController.GetUserById(userId);

                if (user != null)
                {
                    Console.WriteLine($"ID: {user.UserId}, Name: {user.FirstName} {user.LastName}, Active: {user.IsActive}, Admin: {user.IsAdmin}");
                }
                else
                {
                    Console.WriteLine("User not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while finding user: {ex.Message}");
            }
        }
    }
}
