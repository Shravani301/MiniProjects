using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace InventoryManagement.Models
{
    internal class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public string Type { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }

        
        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }

        public Inventory Inventory { get; set; }

        [ForeignKey("Product")]
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public override string ToString()
        {
            return $"InventoryID:{InventoryId}\t ProductId:{ProductId}\t" +
                $" TransactionId:{TransactionId}\t TransactionType:{Type}\t" +
                $"Quantity:{Quantity}\t Date:{Date}\n";
        }
    }
}
