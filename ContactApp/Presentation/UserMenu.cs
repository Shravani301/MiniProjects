using ContactApp.Controller;
using ContactApp.Exceptions;
using ContactApp.Models;
using System;
using System.Collections.Generic;

namespace ContactApp.Presentation
{
    internal static class UserMenu
    {
        private static ContactController contactController = new ContactController();

        public static void DisplayUserMenu(User user)
        {
            while (true)
            {
                Console.WriteLine("User Menu:\n" +
                                  "1. Work on Contacts\n" +
                                  "  1.1 Add new Contact\n" +
                                  "  1.2 Modify Contact\n" +
                                  "  1.3 Delete Contact (soft)\n" +
                                  "  1.4 Display all Contacts\n" +
                                  "  1.5 Find Contact\n" +
                                  "  1.6 Logout\n" +
                                  "2. Work on Contact Details\n" +
                                  "Choose an Option:");

                string choice = Console.ReadLine();
                if (UserFunctionality(choice, user)) break;
            }
        }

        private static bool UserFunctionality(string choice, User user)
        {
            switch (choice)
            {
                case "1.1":
                    AddContact(user);
                    break;
                case "1.2":
                    ModifyContact(user);
                    break;
                case "1.3":
                    DeleteContact(user);
                    break;
                case "1.4":
                    DisplayAllContacts(user);
                    break;
                case "1.5":
                    FindContact(user);
                    break;
                case "1.6":
                    Console.WriteLine("Logging out...");
                    return true;
                case "2":
                    DisplayContactDetailsMenu(user);
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
            return false;
        }

        private static void AddContact(User user)
        {
            try
            {
                Console.Write("Enter Contact Id: ");
                int contactId = int.Parse(Console.ReadLine());
                Console.Write("Enter First Name: ");
                string firstName = Console.ReadLine();
                Console.Write("Enter Last Name: ");
                string lastName = Console.ReadLine();
                Console.Write("Is Active (true/false): ");
                bool isActive = bool.Parse(Console.ReadLine());

                contactController.AddContact(contactId, firstName, lastName, isActive, user);
                Console.WriteLine("Contact added successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter the correct data type.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void ModifyContact(User user)
        {
            try
            {
                Console.Write("Enter Contact Id to modify: ");
                int contactId = int.Parse(Console.ReadLine());

                Console.Write("Enter new First Name (or press Enter to skip): ");
                string firstName = Console.ReadLine();
                Console.Write("Enter new Last Name (or press Enter to skip): ");
                string lastName = Console.ReadLine();
                Console.Write("Set Active Status (true/false) (or press Enter to skip): ");
                string activeInput = Console.ReadLine();
                bool? isActive = string.IsNullOrEmpty(activeInput) ? null : (bool?)bool.Parse(activeInput);

                contactController.ModifyContact(contactId, user, contact =>
                {
                    if (!string.IsNullOrEmpty(firstName)) contact.FirstName = firstName;
                    if (!string.IsNullOrEmpty(lastName)) contact.LastName = lastName;
                    if (isActive.HasValue) contact.IsActive = isActive.Value;
                });
                Console.WriteLine("Contact modified successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter the correct data type.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void DeleteContact(User user)
        {
            try
            {
                Console.Write("Enter Contact Id to delete (soft delete): ");
                int contactId = int.Parse(Console.ReadLine());

                contactController.DeleteContact(contactId, user);
                Console.WriteLine("Contact deleted successfully.");
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter the correct data type.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void DisplayAllContacts(User user)
        {
            try
            {
                IEnumerable<Contact> contacts = contactController.GetAllContacts(user);
                Console.WriteLine("All Contacts:");
                foreach (var contact in contacts)
                {
                    Console.WriteLine($"ID: {contact.ContactId}, Name: {contact.FirstName} {contact.LastName}, Active: {contact.IsActive}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void FindContact(User user)
        {
            try
            {
                Console.Write("Enter Contact Id to find: ");
                int contactId = int.Parse(Console.ReadLine());

                var contact = contactController.GetContactById(contactId, user);
                if (contact != null)
                {
                    Console.WriteLine($"ID: {contact.ContactId}, Name: {contact.FirstName} {contact.LastName}, Active: {contact.IsActive}");
                }
                else
                {
                    Console.WriteLine("Contact not found.");
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input format. Please enter the correct data type.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        private static void DisplayContactDetailsMenu(User user)
        {
            Console.WriteLine("Contact Details Menu:\n" +
                              "1. View Contact Details\n" +
                              "2. Update Contact Details\n" +
                              "3. Back to User Menu\n" +
                              "Choose an option:");

            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    ViewContactDetails(user);
                    break;
                case "2":
                    UpdateContactDetails(user);
                    break;
                case "3":
                    Console.WriteLine("Returning to User Menu...");
                    break;
                default:
                    Console.WriteLine("Invalid option. Please choose a valid option.");
                    break;
            }
        }

        private static void ViewContactDetails(User user)
        {
            Console.WriteLine("Enter Contact ID to view details:");
            int contactId = int.Parse(Console.ReadLine());

            var contact = contactController.GetContactById(contactId, user);
            if (contact != null)
            {
                Console.WriteLine($"ID: {contact.ContactId}, Name: {contact.FirstName} {contact.LastName}, Active: {contact.IsActive}");
                foreach (var detail in contact.ContactDetails)
                {
                    Console.WriteLine($"  - {detail.Type}: {detail.Number}");
                }
            }
            else
                Console.WriteLine("Contact not found.");
        }

        private static void UpdateContactDetails(User user)
        {
            Console.WriteLine("Enter Contact ID to update details:");
            int contactId = int.Parse(Console.ReadLine());

            var contact = contactController.GetContactById(contactId, user);
            if (contact != null)
            {
                Console.Write("Enter new contact detail type (e.g., Phone): ");
                string type = Console.ReadLine();
                Console.Write("Enter new contact detail number: ");
                string number = Console.ReadLine();

                contact.ContactDetails.Add(new ContactDetails { Type = type, Number = number });
                Console.WriteLine("Contact details updated successfully.");
            }
            else
                Console.WriteLine("Contact not found.");
        }
    }
}
