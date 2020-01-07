using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    /// <summary>
    /// User profile entity which contains information about the user.
    /// </summary>
    public class AppUser : IdentityUser<int>
    {
        
        /// <summary>
        /// Id of the user profile.
        /// </summary>
        public override int Id { get; set; }

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

        //public ICollection<AppUserRole> UserRoles { get; set; }

        /// <summary>
        /// Bids placed by the user.
        /// </summary>
        public ICollection<Bid> Bids { get; set; }

        /// <summary>
        /// Lots created by the user.
        /// </summary>
        public ICollection<Lot> Lots { get; set; }

    }
}
