using DAL.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ModelsDTO
{
    /// <summary>
    /// Bid data transfer object which contains information about the bid.
    /// </summary>
    public class BidDTO
    {
        /// <summary>
        /// Id of the bid.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Price of the bid.
        /// </summary>
        [Required]
        public int BidPrice { get; set; }

        /// <summary>
        /// ID of the user who placed the bid.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Name of the user who placed the bid.
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// Id of the lot on which bid was placed.
        /// </summary>
        [Required]
        public int LotId { get; set; }

        /// <summary>
        /// Date when bid was placed.
        /// </summary>
        [Required]
        public DateTime BidDate { get; set; }
    }
}
