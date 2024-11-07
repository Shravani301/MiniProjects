﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Exceptions
{
    internal class SupplierNotFoundException:Exception
    {
        public SupplierNotFoundException(string message) : base(message)
        {
        }
    }
}