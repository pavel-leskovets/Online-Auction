using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Exeptions
{
    /// <summary>
    /// The exception is thrown when requested entity doesn't exist in database.
    /// </summary>
    class NotFoundException : Exception
    {
        public NotFoundException() : base() { }
        public NotFoundException(string str) : base(str) { }
    }
}
