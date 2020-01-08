using DAL.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BLL.ModelsDTO
{
    /// <summary>
    /// Lot data transfer object which contains information about the lot.
    /// </summary>
    public class LotDTO
    {
        /// <summary>
        /// Id of the lot.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the lot.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Initial price of the lot.
        /// </summary>
        [Required]
        public int InitialPrice { get; set; }

        /// <summary>
        /// Current maximum price.
        /// </summary>
        public decimal CurrentPrice { get; set; }

        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Date when the auction is starting.
        /// </summary>
        [Required]
        public DateTime BeginDate { get; set; }

        /// <summary>
        /// Date when the auction is ending.
        /// </summary>
        [Required]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// ID of the user who created the lot.
        /// </summary>
        [Required]
        public int UserId { get; set; }

        /// <summary>
        /// Description of the lot.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Represents a photo sent with the HttpRequest.
        /// </summary>
        public IFormFile Image { get; set; }

        /// <summary>
        /// Path to photo of the lot.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Bids placed on the lot.
        /// </summary>
        public ICollection<Bid> Bids { get; set; }
    }
}
