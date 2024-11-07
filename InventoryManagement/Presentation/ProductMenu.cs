using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using InventoryManagement.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Presentation
{
    public class ProductMenu
    {
        private ProductRepository _repository = new ProductRepository();

        public void DisplayProductMenu()
        {
            bool exit = false;

            while (!exit)
            {
                try
                {
                    Console.WriteLine("Product Management Menu:\n" +
                                      "1. Add Product\n" +
                                      "2. Update Product\n" +
                                      "3. Delete Product\n" +
                                      "4. View Product's Details\n" +
                                      "5. View All Products\n" +
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
                    AddProduct();
                    break;
                case 2:
                    UpdateProduct();
                    break;
                case 3:
                    DeleteProduct();
                    break;
                case 4:
                    ViewProductDetails();
                    break;
                case 5:
                    ViewAllProducts();
                    break;
                case 6:
                    return true;
                default:
                    Console.WriteLine("Invalid choice, please select a valid option.");
                    break;
            }
            return false;
        }

        private void AddProduct()
        {
            try
            {
                Console.WriteLine("Adding product...");
                Console.WriteLine("Enter Product Name:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter description:");
                string desc = Console.ReadLine();
                Console.WriteLine("Enter Quantity:");
                int quantity = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Price of each product:");
                double price = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Enter InventoryID:");
                int IId = Convert.ToInt32(Console.ReadLine());

                // Instantiate a new product for each Add operation
                var product = new Product()
                {
                    Name = name,
                    Description = desc,
                    Price = price,
                    Quantity = quantity,
                    InventoryId = IId
                };

                _repository.AddProduct(product);
                Console.WriteLine("Product added successfully");
            }
            catch (ProductAlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
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

        private void UpdateProduct()
        {
            try
            {
                Console.WriteLine("Updating product...\n Enter ProductID:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Enter Product name to update an existing product:");
                string name = Console.ReadLine();
                Console.WriteLine("Enter description to update the existing product:");
                string desc = Console.ReadLine();
                Console.WriteLine("Enter product price:");
                double price = Convert.ToDouble(Console.ReadLine());

                // Instantiate a new product for each Update operation
                var product = new Product()
                {
                    ProductId = id,
                    Name = name,
                    Description = desc,
                    Price = price
                };

                _repository.UpdateProduct(product);
                Console.WriteLine("Product updated successfully");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (ProductAlreadyExistException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteProduct()
        {
            try
            {
                Console.WriteLine("Enter productId for deletion:");
                int id = Convert.ToInt32(Console.ReadLine());
                _repository.DeleteProduct(id);
                Console.WriteLine("Product deleted successfully");
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ViewProductDetails()
        {
            try
            {
                Console.WriteLine("Enter productId to display product details:");
                int id = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine(_repository.GetProduct(id));
            }
            catch (ProductNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void ViewAllProducts()
        {
            Console.WriteLine("Available Products are:");
            var products = _repository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine(product);
            }
        }
    }

}
