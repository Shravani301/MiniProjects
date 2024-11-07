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
    internal class ProductRepository
    {
        private InventoryContext _context;
        public ProductRepository()
        {
            _context = new InventoryContext();
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.ToList();
        }

        public Product GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                throw new ProductNotFoundException("Product not found");
            return product;
        }

        public void AddProduct(Product product)
        {
            var inventoryExists = _context.Inventories.Any(i => i.InventoryId == product.InventoryId);

            if (!inventoryExists)
            {
                throw new InventoryNotFoundException($"Inventory with ID {product.InventoryId} does not exist.");
            }
            if (_context.Products.Any(p => p.Name == product.Name))
                throw new ProductAlreadyExistException("Product name already exists.");

            _context.Products.Add(product);
            _context.SaveChanges();

            var transaction = new Transaction
            {
                ProductId = product.ProductId,  
                InventoryId = product.InventoryId, 
                Quantity = product.Quantity,  
                Type = "AddStock", 
                Date = DateTime.Now  
            };

            _context.Transactions.Add(transaction);
            _context.SaveChanges(); 
        }

        public void UpdateProduct(Product product)
        {

            var existingProduct = GetProduct(product.ProductId);
            if (existingProduct == null)
                throw new ProductNotFoundException("Product not found");

            if (_context.Products.Any(p => p.Name == product.Name && p.ProductId != product.ProductId))
                throw new ProductAlreadyExistException("Product name already exists.");

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = GetProduct(id);
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

    }
}
