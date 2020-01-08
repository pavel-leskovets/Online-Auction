using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exceptions
{
    /// <summary>
    /// The exception is thrown when requested entity doesn't exist in database.
    /// </summary>
    public class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string str) : base(str) { }
    }
}
