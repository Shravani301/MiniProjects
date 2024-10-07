using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountLibrary.Exceptions
{
    public class ZeroAccountsException : Exception
    {
        public ZeroAccountsException(string message) : base(message) { }
    }
}
