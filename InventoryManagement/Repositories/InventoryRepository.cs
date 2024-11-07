using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    internal class InventoryRepository
    {
        private readonly InventoryContext _context;

        public InventoryRepository()
        {
            _context = new InventoryContext();
        }

        public List<Inventory> GetAllInventories()
        {
           return _context.Inventories
                .Include(i => i.Products)
                .Include(i => i.Suppliers)
                .Include(i => i.Transactions)
                .ToList();
        }

        public Inventory GetInventoryById(int inventoryId)
        {
            return _context.Inventories
                .Include(i => i.Products)
                .Include(i => i.Suppliers)
                .Include(i => i.Transactions)
                .FirstOrDefault(i => i.InventoryId == inventoryId);
        }

        public List<Product> GetProductsByInventoryId(int inventoryId)
        {
            return _context.Products
                .Where(p => p.InventoryId == inventoryId)
                .ToList();
        }


        public List<Supplier> GetSuppliersByInventoryId(int inventoryId)
        {
            return _context.Suppliers
                .Where(s => s.InventoryId == inventoryId)
                .ToList();
        }

        public List<Transaction> GetTransactionsByInventoryId(int inventoryId)
        {
            return _context.Transactions
                .Where(t => t.InventoryId == inventoryId)
                .ToList();
        }

        public void GenerateReport()
        {
            var inventories = GetAllInventories();

            foreach (var inventory in inventories)
            {
                var totalStockValue = inventory.Products.Sum(p => p.Quantity * p.Price);

                Console.WriteLine($"===== Inventory {inventory.InventoryId} Summary =====");
                Console.WriteLine($"Location: {inventory.Location}");
                Console.WriteLine($"Total Products: {inventory.Products.Count}");
                Console.WriteLine($"Total Suppliers: {inventory.Suppliers.Count}");
                Console.WriteLine($"Total Transactions: {inventory.Transactions.Count}");
                Console.WriteLine($"Total Stock Value: ${totalStockValue:F2}");
                Console.WriteLine("====================================\n");

                // Product Details
                Console.WriteLine("===== Product Details =====");
                foreach (var product in inventory.Products)
                {
                    // Display product information with stock value
                    var productStockValue = product.Quantity * product.Price;
                    Console.WriteLine($"ID: {product.ProductId}, Name: {product.Name}, Quantity: {product.Quantity}, Price: {product.Price}, Stock Value: ${productStockValue:F2}");
                }

                Console.WriteLine("\n===== Supplier Details =====");
                foreach (var supplier in inventory.Suppliers)
                {
                    Console.WriteLine($"SupplierID: {supplier.SupplierId}, Name: {supplier.Name}, ContactInfo: {supplier.ContactInfo}");
                }

                Console.WriteLine("\n===== Transaction Details =====");
                foreach (var transaction in inventory.Transactions)
                {
                    Console.WriteLine($"TransactionID: {transaction.TransactionId}, Type: {transaction.Type}, Quantity: {transaction.Quantity}, Date: {transaction.Date}");
                }

                Console.WriteLine("====================================\n");
            }
        }

    }
}
