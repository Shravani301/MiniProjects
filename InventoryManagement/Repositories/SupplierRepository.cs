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
    internal class SupplierRepository
    {
        private InventoryContext _context;
        public SupplierRepository()
        {
            _context = new InventoryContext();
        }

        public List<Supplier> GetAllSuppliers()
        {
            return _context.Suppliers.ToList();
        }

        public Supplier GetSupplier(int id)
        {
            var supplier = _context.Suppliers.Find(id);
            if (supplier == null)
                throw new SupplierNotFoundException("Supplier not found");
            return supplier;
        }

        public void AddSupplier(Supplier supplier)
        {
            var inventoryExists = _context.Inventories.Any(i => i.InventoryId == supplier.InventoryId);

            if (!inventoryExists)
            {
                throw new InventoryNotFoundException($"Inventory with ID {supplier.InventoryId} does not exist.");
            }
            if (_context.Suppliers.Any(s => s.Name == supplier.Name))
                throw new SupplierAlreadyExistException("Supplier name already exists.");
            _context.Suppliers.Add(supplier);
            _context.SaveChanges();
        }

        public void UpdateSupplier(Supplier supplier)
        {
            var existingSupplier = GetSupplier(supplier.SupplierId);
            
            if (_context.Suppliers.Any(s => s.Name == supplier.Name && s.SupplierId != supplier.SupplierId))
                throw new SupplierAlreadyExistException("Supplier name already exists.");

            existingSupplier.Name = supplier.Name;
            existingSupplier.ContactInfo = supplier.ContactInfo;

            _context.SaveChanges();
        }

        public void DeleteSupplier(int id)
        {
            var supplier = GetSupplier(id);
            _context.Suppliers.Remove(supplier);
            _context.SaveChanges();
        }

    }
}
