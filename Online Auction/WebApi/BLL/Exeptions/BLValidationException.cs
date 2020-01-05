using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exeptions
{
    /// <summary>
    /// The exception is thrown when received data don't pass business logic validation.
    /// </summary>
    class BLValidationException : Exception
    {
        public BLValidationException() : base() { }
        public BLValidationException(string str) : base(str) { }
    }
}
