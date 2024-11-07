using InventoryManagement.Data;
using InventoryManagement.Exceptions;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repositories
{
    internal class TransactionRepository
    {
        private readonly InventoryContext _context;

        public TransactionRepository()
        {
            _context = new InventoryContext();
        }

        public List<Transaction> GetAllTransactions()
        {
            return _context.Transactions.ToList();
        }

        public List<Transaction> GetTransactionsByProductId(int productId)
        {
            var product = _context.Products.Find(productId);
            if (product == null)
                throw new ProductNotFoundException("Product not found");

            return _context.Transactions
                           .Where(t => t.ProductId == productId)
                           .OrderByDescending(t => t.Date)
                           .ToList();
        }

        public void AddTransaction(int productId, int quantityChange, string type)
        {
            var product = _context.Products.Include(p => p.Inventory).FirstOrDefault(p => p.ProductId == productId);
            if (product == null)
                throw new ProductNotFoundException("Product not found");

            if (product.Inventory == null)
                throw new Exception("Product does not have an associated inventory.");

            if (quantityChange < 0 && product.Quantity + quantityChange < 0)
                throw new InsufficientStockException("Not enough stock available.");

            product.Quantity += quantityChange;

            var transaction = new Transaction
            {
                ProductId = productId,
                InventoryId = product.Inventory.InventoryId, 
                Quantity = quantityChange,
                Type = type, 
                Date = DateTime.Now
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges();
        }


    }
}
