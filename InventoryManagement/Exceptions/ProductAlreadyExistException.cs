using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    internal class ProductAlreadyExistException:Exception
    {
        public ProductAlreadyExistException(string message) : base(message)
        {
        }
    }
}
