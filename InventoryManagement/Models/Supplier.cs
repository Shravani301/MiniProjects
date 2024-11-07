using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Models
{
    internal class Supplier
    {
        [Key]
        public int SupplierId { get; set; }

        public string Name { get; set; }
        public string ContactInfo { get; set; }

        [ForeignKey("Inventory")]
        public int InventoryId { get; set; }        
        public Inventory Inventory { get; set; }
        public override string ToString()
        {
            return $"SupplierID:{SupplierId}\t Name:{Name}\t ContactInfo:{ContactInfo}\t Inventory:{InventoryId}\n ";
        }
    }
}
