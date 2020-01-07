using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    /// <summary>
    /// Lot entity.
    /// </summary>
    public class Lot
    {
        /// <summary>
        /// Id of the lot.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the lot.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Initial price of the lot.
        /// </summary>
        public int InitialPrice { get; set; }

        ///// <summary>
        ///// Current price of the lot.
        ///// </summary>
        //public decimal CurrentPrice { get; set; }

        /// <summary>
        /// Category Id where the lot was created.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Date when the auction is starting.
        /// </summary>
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Date when the auction is ending.
        /// </summary>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Id of the user who created the lot.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Description of the lot.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Address of the lot photo.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Category where the lot was created.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// User who created the lot.
        /// </summary>
        public AppUser AppUser { get; set; }
        
        /// <summary>
        /// Bids placed on the lot.
        /// </summary>
        public ICollection<Bid> Bids { get; set; }

    }
}
        