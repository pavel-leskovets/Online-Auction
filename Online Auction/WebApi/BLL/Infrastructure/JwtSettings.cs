using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Infrastructure
{
    /// <summary>
    /// Class for configuring JSON web token
    /// </summary>
    public class JwtSettings
    {
        /// <summary>
        /// Secret key.
        /// </summary>
        public string JwtKey { get; set; }

        /// <summary>
        /// The validity period of token. 
        /// </summary>
        public TimeSpan LifeTime { get; set; }
    }
}
