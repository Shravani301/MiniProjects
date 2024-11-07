using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using System;
using System.Collections.Generic;

namespace InventoryManagement.Presentation
{
    public class SupplierMenu
    {
        private SupplierRepository _repository = new SupplierRepository();

        public void DisplaySupplierMenu()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.WriteLine("Supplier Management Menu:\n" +
                                      "1. Add Supplier\n" +
                                      "2. Update Supplier\n" +
                                      "3. Delete Supplier\n" +
                                      "4. View Supplier's Details\n" +
                                      "5. View All Suppliers\n" +
                                      "6. Exit\n" +
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
                    AddSupplier();
                    break;
                case 2:
                    UpdateSupplier();
                    break;
                case 3:
                    DeleteSupplier();
                    break;
                case 4:
                    ViewSupplierDetails();
                    break;
                case 5:
                    ViewAllSuppliers();
                    break;
                case 6:
                    Console.WriteLine("Returning to Main Menu...");
                    return true;
                default:
                    Console.WriteLine("Invalid choice, please select a valid option.");
                    break;
            }
            return false;
        }

        private void AddSupplier()
        {
            
            Console.WriteLine("Adding supplier...");
            Console.WriteLine("Enter Supplier Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter Contact Information:");
            string contactInfo = Console.ReadLine();
            Console.WriteLine("Enter Inventory id:");
            int inventoryId=Convert.ToInt32(Console.ReadLine());

            var supplier = new Supplier
            {
                Name = name,
                ContactInfo = contactInfo,
                InventoryId = inventoryId
            };

            try
            {
                _repository.AddSupplier(supplier);
                Console.WriteLine("Supplier added successfully.");
            }
            catch (InventoryNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            
        }

        private void UpdateSupplier()
        {
            Console.WriteLine("Updating supplier...\nEnter Supplier ID:");
            int id = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter New Supplier Name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter New Contact Information:");
            string contactInfo = Console.ReadLine();
            Console.WriteLine("Enter inventory Id:");
            int inventoryId=Convert.ToInt32(Console.ReadLine());

            var supplier = new Supplier
            {
                SupplierId = id,
                Name = name,
                ContactInfo = contactInfo,
                InventoryId = inventoryId,
            };

            try
            {
                _repository.UpdateSupplier(supplier);
                Console.WriteLine("Supplier updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void DeleteSupplier()
        {
            Console.WriteLine("Enter Supplier ID for deletion:");
            int id = Convert.ToInt32(Console.ReadLine());

            try
            {
                _repository.DeleteSupplier(id);
                Console.WriteLine("Supplier deleted successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ViewSupplierDetails()
        {
            Console.WriteLine("Enter Supplier ID to view details:");
            int id = Convert.ToInt32(Console.ReadLine());

            try
            {
                var supplier = _repository.GetSupplier(id);
                Console.WriteLine(supplier);
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void ViewAllSuppliers()
        {
            Console.WriteLine("Available Suppliers:");
            var suppliers = _repository.GetAllSuppliers();

            foreach (var supplier in suppliers)
            {
                Console.WriteLine(supplier);
               
            }
        }
    }
}
