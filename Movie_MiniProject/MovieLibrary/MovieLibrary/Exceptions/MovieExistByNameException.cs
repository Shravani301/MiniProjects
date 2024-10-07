using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLibrary.Exceptions
{
    public class MovieExistByNameException:Exception
    {
        public MovieExistByNameException(string message) 
            : base(message) { }
    }
}
