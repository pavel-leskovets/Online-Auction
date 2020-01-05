using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.ModelsDTO;
using BLL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    /// <summary>
    /// Categories controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get all categories.
        /// </summary>
        /// <returns>200 - at least 1 category found; 204 - no categories found.</returns>
        [HttpGet]
        public ActionResult<IEnumerable<CategoryDTO>> GetAllCategories()
        {
            var categories = _categoryService.GetCategories();
            if (!categories.Any())
            {
                return NoContent();
            }
            return Ok(categories);
        }

        /// <summary>
        /// Get category by id.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>200 - category found; 404 - category not found.</returns>
        [HttpGet("{id}")]
        public ActionResult<CategoryDTO> GetCategory(int id)
        {
            var category = _categoryService.GetCategory(id);
            if (category == null)
            {
                return NotFound();
            }
            return Ok(category);
        }

        /// <summary>
        /// Get lots by category id.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>200 - lots found; 204 - lots not found.</returns>
        [HttpGet("{id}/lots")]
        public ActionResult<IEnumerable<LotDTO>> GetLotsByCategory(int id)
        {
            var lots = _categoryService.GetLotsByCategory(id);
            if (!lots.Any())
            {
                return NoContent();
            }
            return Ok(lots);
        }
               
        /// <summary>
        /// Create category.
        /// </summary>
        /// <param name="category">New category.</param>
        /// <returns>400 - validation failed, 201 - category created.</returns>
        [HttpPost]
        public ActionResult CreateCategory([FromBody] CategoryDTO category)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var created = _categoryService.CreateCategory(category);
            return CreatedAtAction(nameof(GetCategory), new { id = created.Id }, created);
        }

        /// <summary>
        /// Edit category.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <param name="category">Category.</param>
        /// <returns>400 - validation failed; 204 - category updated; 404 - category not found.</returns>
        [HttpPut("{id}")]
        public ActionResult EditCategory(int id, [FromBody] CategoryDTO category)
        {
            if (id != category.Id)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                _categoryService.EditCategory(category);
            }
            catch (Exception ex)
            {
                if (_categoryService.GetCategory(id) == null)
                {
                    return NotFound();
                }
                return BadRequest(ex.Message);
            }
            return NoContent();
        }

        // <summary>
        /// Delete category by ID.
        /// </summary>
        /// <param name="id">Category ID.</param>
        /// <returns>404 - category not found; 200 - category deleted.</returns>
        [HttpDelete("{id}")]
        public ActionResult<CategoryDTO> DeleteCategory(int id)
        {
            var toDelete = _categoryService.GetCategory(id);
            if (toDelete == null)
            {
                return NotFound();
            }
            _categoryService.DeleteCategory(id);
            return toDelete;
        }
    }
}
