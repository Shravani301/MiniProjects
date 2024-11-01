using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactApp.Exceptions
{
    public class InvalidUserActionException : Exception
    {
        public InvalidUserActionException(string message) : base(message) { }
    }
}
