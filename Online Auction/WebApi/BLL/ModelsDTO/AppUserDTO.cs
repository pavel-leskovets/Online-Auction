using BLL.Infrastructure;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ModelsDTO
{
    /// <summary>
    /// User data transfer object which contains information about the user.
    /// </summary>
    public class AppUserDTO
    {
        /// <summary>
        /// Id of the user profile.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Sign-in name of the user.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// First name of the user.
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the user.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Address of the user.
        /// </summary>
        public string Addres { get; set; }

        /// <summary>
        /// Phone number of the user.
        /// </summary>
        public string Phone { get; set; }

        public string Role { get; set; }
    }
}
