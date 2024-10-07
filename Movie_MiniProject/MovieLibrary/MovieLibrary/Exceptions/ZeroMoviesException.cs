using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Exceptions
{
    public class ZeroMoviesException : Exception
    {
        public ZeroMoviesException(string message)
        : base(message) { }
    }
}
