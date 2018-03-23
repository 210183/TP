﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.DataHandling
{
    [Serializable]
    public class DuplicateException : ArgumentException
    {
        public DuplicateException(string message) : base(message)
        {
        }
    }
}
