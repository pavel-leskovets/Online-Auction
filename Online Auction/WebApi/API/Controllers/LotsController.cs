using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Services;
using BLL.ModelsDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using DAL.Models;
using BLL.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using System.ComponentModel.DataAnnotations;
using API.Infrastructure;

namespace BLL.Controllers
{
    /// <summary>
    /// Lots controller.
    /// </summary>
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LotsController : ControllerBase
    {
        private IWebHostEnvironment _environment;

        private ILotService _lotService;
        public LotsController(ILotService lotService, IWebHostEnvironment environment)
        {
            _lotService = lotService;
            _environment = environment;
        }

        /// <summary>
        /// Get all lots.
        /// </summary>
        /// <returns>200 - lots found.</returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<IEnumerable<LotDTO>> GetAllLots()
        {
            var lots = _lotService.GetAllLots();
            return Ok(lots);
        }


        /// <summary>
        /// Get lot by ID.
        /// </summary>
        /// <param name="id">Lot ID.</param>
        /// <returns>200 - lot found; 404 - lot not found.</returns>
        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult<LotDTO> GetLot(int id)
        {
            var lot = _lotService.GetLot(id);
            if (lot == null)
            {
                return NotFound();
            }
            return Ok(lot);
        }


        /// <summary>
        /// Create new lot.
        /// </summary>
        /// <param name="lot">New lot.</param>
        /// <returns>201 - lot created; 400 - lot validation failed.</returns>
        [HttpPost]
        public ActionResult<LotDTO> CreateLot([FromForm]LotDTO lot)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (lot.Image != null)
            {
                if (!lot.Image.ContentType.Contains("image"))
                {
                    return BadRequest("Invalid image format.");
                }

                try
                {
                    lot.ImageUrl = ImageHandler.SaveImage(lot.Image, _environment);
                }
                catch (Exception)
                {
                    return BadRequest("Image upload error.");
                }
            }

            LotDTO created;
            try
            {
                created = _lotService.CreateLot(lot);
            }
            catch (Exception ex)
            {
                return BadRequest("Validation error. " + ex.Message);
            }
            return CreatedAtAction(nameof(GetLot), new { id = created.Id }, created);
        }



        /// <summary>
        /// Update lot.
        /// </summary>
        /// <param name="id">Lot ID.</param>
        /// <param name="lot">Lot.</param>
        /// <returns>204 - lot updated; 400 - lot validation failed; 404 - lot not found.</returns>
        [HttpPut("{id}")]
        public ActionResult EditLot(int id, [FromForm] LotDTO lot)
        {
            if (id != lot.Id)
                return BadRequest();
            
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

           

            if (lot.Image != null)
            {
                if (!lot.Image.ContentType.Contains("image"))
                    return BadRequest("Invalid image format.");

                try
                {
                    lot.ImageUrl = ImageHandler.SaveImage(lot.Image, _environment);
                }
                catch (Exception)
                {
                    return BadRequest("Image upload error.");
                }
            }
            
           
            try
            {
                _lotService.UpdateLot(lot);
            }
            catch (Exception ex)
            {
                if (_lotService.GetLot(id) == null)
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }
            return NoContent();
        }

        /// <summary>
        /// Delete lot.
        /// </summary>
        /// <param name="id">Lot ID.</param>
        /// <returns>404 - lot not found; 200 - lot deleted.</returns>
        [HttpDelete("{id}")]
        public ActionResult<LotDTO> Delete(int id)
        {
            var toDelete = _lotService.GetLot(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            _lotService.DeleteLot(id);
            return NoContent();
        }
    }
}
