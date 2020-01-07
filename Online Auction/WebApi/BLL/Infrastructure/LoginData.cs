using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    /// <summary>
    /// Class for authentication user.
    /// </summary>
    public class LoginData
    {
        /// <summary>
        /// User name
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// User password
        /// </summary>
        public string Password { get; set; }
    }
}
