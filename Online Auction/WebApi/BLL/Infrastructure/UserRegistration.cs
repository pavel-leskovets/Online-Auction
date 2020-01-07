using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    /// <summary>
    /// New user registration model.
    /// </summary>
    public class UserRegistration
    {
        /// <summary>
        /// The user name.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// The user email.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// The user password.
        /// </summary>
        [Required]
        public string Password { get; set; }
        
    }
}
