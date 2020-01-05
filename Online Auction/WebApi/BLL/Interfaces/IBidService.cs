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
    public interface IBidService
    {

        IEnumerable<BidDTO> GetBids();

        /// <summary>
        /// Method for getting bid by ID.
        /// </summary>
        /// <param name="id">The bid ID.</param>
        /// <returns>Bid DTO.</returns>
        BidDTO GetBid(int id);
        BidDTO CreateBid(BidDTO item);

        /// <summary>
        /// Method for deleting bid by ID.
        /// </summary>
        /// <param name="id">The bid ID.</param>
        void Delete(int id);
    }
}
