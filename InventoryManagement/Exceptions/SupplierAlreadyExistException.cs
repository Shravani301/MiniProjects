using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    internal class SupplierAlreadyExistException:Exception
    {
        public SupplierAlreadyExistException(string message) : base(message) { }
    }
}
