using BLL.ModelsDTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Services
{
    /// <summary>
    /// Interface for bids service.
    /// Contains methods for managing bids.
    /// </summary>
    public interface IBidService : IDisposable
    {
        /// <summary>
        /// Method for getting all bids.
        /// </summary>
        /// <returns>Collection of bids DTOs.</returns>
        IEnumerable<BidDTO> GetAllBids();

        /// <summary>
        /// Method for getting bid by ID.
        /// </summary>
        /// <param name="id">The bid ID.</param>
        /// <returns>Bid DTO.</returns>
        BidDTO GetBid(int id);

        /// <summary>
        /// Method for creating bid.
        /// </summary>
        /// <param name="bid">The bid DTO.</param>
        /// <returns>Created bid DTO.</returns>
        BidDTO CreateBid(BidDTO bid);

        /// <summary>
        /// Method for deleting bid by ID.
        /// </summary>
        /// <param name="id">The bid ID.</param>
        void DeleteBid(int id);
    }
}
