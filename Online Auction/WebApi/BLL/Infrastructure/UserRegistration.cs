using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class UserRegistration
    {
        [Required]
       
        public string UserName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MinLength(4)]
        public string Password { get; set; }
        
    }
}
