using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.ModelsDTO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Bids controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BidsController : ControllerBase
    {
        private readonly IBidService _bidService;
        public BidsController(IBidService bidService)
        {
            _bidService = bidService;
        }

        /// <summary>
        /// Get all Bids.
        /// </summary>
        /// <returns>200 - lots found.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<BidDTO>> GetAllBids()
        {
            var bids = _bidService.GetAllBids();
            return Ok(bids);
        }

        /// <summary>
        /// Get bid by ID.
        /// </summary>
        /// <param name="id">Bid ID.</param>
        /// <returns>200 - bid found; 404 - bid not found.</returns>  
        [HttpGet("{id}")]
        public ActionResult<BidDTO> GetBid(int id)
        {
            var bid = _bidService.GetBid(id);
            if (bid == null)
            {
                return NotFound();
            }
            return Ok(bid);
        }

        /// <summary>
        /// Create new bid on the lot.
        /// </summary>
        /// <param name="bid">New bid</param>
        /// <returns>400 - validation failed; 201 - bid created.</returns>
        [HttpPost]
        public ActionResult CreateBid([FromBody] BidDTO bid)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            BidDTO created;
            try
            {
                created = _bidService.Create(bid);
            }
            catch (Exception ex)
            {
                return BadRequest("Validation error. " + ex.Message); 
            }
            return CreatedAtAction(nameof(GetBid), new { id = created.Id }, created);
        }

        /// <summary>
        /// Delete bid by ID.
        /// </summary>
        /// <param name="id">Bid ID.</param>
        /// <returns>404 - bid not found; 200 - bid deleted.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin, Moderator")]
        public ActionResult<BidDTO> DeleteBid(int id)
        {
            var toDelete = _bidService.GetBid(id);

            if (toDelete == null)
            {
                return NotFound();
            }
            _bidService.Delete(id);
            return toDelete;
        }
    }
}
