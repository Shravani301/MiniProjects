using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    internal class InventoryNotFoundException:Exception
    {
        public InventoryNotFoundException(string message):base(message) { }
    }
}
