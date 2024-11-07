using InventoryManagement.Repositories;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Presentation
{
    internal class Menu
    {
        public void DisplayMenu()
        {
            bool exit = false;

            while (!exit)
            {
                Console.WriteLine("Welcome to the Inventory Management System\n" +
                                  "1. Product Management\n" +
                                  "2. Supplier Management\n" +
                                  "3. Transaction Management\n" +
                                  "4. Generate Report\n" +
                                  "5. Exit\n" +
                                  "Enter your choice:");

                try
                {
                    int choice = Convert.ToInt32(Console.ReadLine());

                    exit=DoTask(choice);    
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input, please enter a number.");
                }
            }
            
        }
        private bool DoTask(int choice)
        {
            switch (choice)
            {
                case 1:
                    new ProductMenu().DisplayProductMenu();
                    break;
                case 2:
                    new SupplierMenu().DisplaySupplierMenu();
                    break;
                case 3:
                    new TransactionMenu().DisplayTransactionMenu();
                    break;
                case 4:
                    GenerateReport1();
                    break;
                case 5:
                    return true;
                default:
                    Console.WriteLine("Invalid choice, please select a valid option.");
                    break;
            }
            return false;
        }

        private void GenerateReport1()
        {
            new InventoryRepository().GenerateReport();
        }
    }
}

