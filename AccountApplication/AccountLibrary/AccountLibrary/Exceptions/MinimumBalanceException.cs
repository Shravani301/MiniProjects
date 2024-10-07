using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccountLibrary.Exceptions
{
    public class MinimumBalanceException : Exception
    {
        public MinimumBalanceException(string message) : base(message) { }
    }
}
