using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.Models
{
    /// <summary>
    /// Bid entity.
    /// </summary>
    public class Bid
    {
        /// <summary>
        /// Id of the bid.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The bid price.
        /// </summary>
        public int BidPrice { get; set; }

        /// <summary>
        /// Id of the user profile who placed bid.
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Name of the user profile who placed bid.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Id of the lot where the bid was placed.
        /// </summary>
        public int LotId { get; set; }

        /// <summary>
        /// Date when the bid was placed.
        /// </summary>
        public DateTime BidDate { get; set; }

     

    }
}
