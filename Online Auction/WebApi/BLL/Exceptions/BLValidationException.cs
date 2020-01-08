using System;

namespace BLL.Exceptions
{
    /// <summary>
    /// The exception is thrown when received data don't pass business logic validation.
    /// </summary>
    public class BLValidationException : Exception
    {
        public BLValidationException() : base() { }
        public BLValidationException(string str) : base(str) { }
    }
}
