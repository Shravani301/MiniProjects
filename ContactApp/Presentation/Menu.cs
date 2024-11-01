using ContactApp.Controller;
using ContactApp.Exceptions;
using System;

namespace ContactApp.Presentation
{
    internal class Menu
    {
        private static UserController userController = new UserController();

        public static void DisplayMainMenu()
        {
            Console.Write("Enter UserId: ");
            if (!int.TryParse(Console.ReadLine(), out int userId))
            {
                Console.WriteLine("Invalid input. Please enter a valid UserId.");
                return;
            }

            try
            {
                var user = userController.GetUserById(userId);
                if (userController.IsAdmin(userId))
                {
                    AdminMenu.DisplayAdminMenu();
                }
                else
                {
                    UserMenu.DisplayUserMenu(user);
                }
            }
            catch (UserNotFoundException e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
