using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    internal class TransactionNotFoundException:Exception
    {
        public TransactionNotFoundException(string message) : base(message) { } 
    }
}
