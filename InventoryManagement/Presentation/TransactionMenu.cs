using InventoryManagement.Exceptions;
using InventoryManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Presentation
{
    public class TransactionMenu
    {
        private TransactionRepository _transactionRepository = new TransactionRepository();

        public void DisplayTransactionMenu()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.WriteLine("Transaction Management Menu:\n" +
                                      "1. Add Stock to Product\n" +
                                      "2. Remove Stock from Product\n" +
                                      "3. View Transaction History of a Product\n" +
                                      "4. Go Back to Main Menu\n" +
                                      "Enter your choice:");

                    int choice = Convert.ToInt32(Console.ReadLine());
                    exit = DoTask(choice);
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
                    AddStock();
                    break;
                case 2:
                    RemoveStock();
                    break;
                case 3:
                    ViewTransactionHistory();
                    break;
                case 4:
                    Console.WriteLine("Returning to Main Menu...");
                    return true;
                default:
                    Console.WriteLine("Invalid choice, please select a valid option.");
                    break;
            }
            return false;
        }

        private void AddStock()
        {
            try
            {
                Console.WriteLine("Enter Product ID to add stock:");
                int productId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter quantity to add:");
                int quantity = Convert.ToInt32(Console.ReadLine());

                // Add transaction for adding stock with type "Addition"
                _transactionRepository.AddTransaction(productId, quantity, "Addition");
                Console.WriteLine("Stock added successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine($"Product not found: {ex.Message}");
            }
            catch (InsufficientStockException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                    Console.WriteLine($"Stack Trace: {ex.InnerException.StackTrace}");
                }
            }
        }

        private void RemoveStock()
        {
            try
            {
                Console.WriteLine("Enter Product ID to remove stock:");
                int productId = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter quantity to remove:");
                int quantity = Convert.ToInt32(Console.ReadLine());

                _transactionRepository.AddTransaction(productId, -quantity, "Removal");
                Console.WriteLine("Stock removed successfully.");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine($"Product not found: {ex.Message}");
            }
            catch (InsufficientStockException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
            }
        }


        private void ViewTransactionHistory()
        {
            try
            {
                Console.WriteLine("Enter Product ID to view transaction history:");
                int productId = Convert.ToInt32(Console.ReadLine());

                var transactions = _transactionRepository.GetTransactionsByProductId(productId);
                Console.WriteLine("Transaction History:");
                foreach (var transaction in transactions)
                {
                    Console.WriteLine(transaction);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

}
