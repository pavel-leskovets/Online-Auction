using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Infrastructure;
using BLL.ModelsDTO;
using BLL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Controllers
{
    /// <summary>
    /// Users controller.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get all users with pagination.
        /// </summary>
        /// <param name="model">Pagination model.</param>
        /// <returns>200 - at least 1 user found; 204 - no users found.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<AppUserDTO>> GetUsers()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet("Profile")]
        public async Task<ActionResult<AppUserDTO>> GetProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userService.GetUserProfileAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<AppUserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserProfileAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }



        [HttpPut("{id}")]
        
        public async Task<ActionResult<AppUserDTO>> UpdateProfile([FromBody] AppUserDTO user, int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (user.Id != id)
            {
                return BadRequest();
            }

            try
            {
                await _userService.UpdateProfileAsync(user);
            }
            catch (Exception)
            {

                return BadRequest();
            }
                       
            return Ok(user);
        }

        /// <summary>
        /// Get lots by user profile.
        /// </summary>
        /// <returns>200 lots found; 204 - no lots found; 404 - user not found.</returns>
        [HttpGet("Profile/Lots")]
        public async Task<ActionResult<IEnumerable<LotDTO>>> GetLotsByProfile()
        {
            var userId = User.Claims.First(c => c.Type == "UserId").Value;
            var user = await _userService.GetUserProfileAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }
            var lots = _userService.GetLotsByProfileAsync(userId);
            if (!lots.Any())
            {
                return NoContent();
            }
            return Ok(lots);
        }


        /// <summary>
        /// Get bids by user profile.
        /// </summary>
        /// <returns>200 - at least 1 bid found; 204 - no bids found; 404 - user not found.</returns>
        [HttpGet("Profile/Bids")]
        public async Task<ActionResult<IEnumerable<BidDTO>>> GetBidsByProfile()
        {
            string userId = User.Claims.First(c => c.Type == "UserId").Value;

            var user = await _userService.GetUserProfileAsync(userId);

            if (user == null)
            {
                return NotFound("User not found");
            }

            var bids = _userService.GetBidsByProfile(user.Id);
            if (!bids.Any())
            {
                return NoContent();
            }
            return Ok(bids);
        }


        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<ActionResult> RegisterUserAsync([FromBody]UserRegistration regData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _userService.CreateUserAsync(regData);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult> LoginAsync([FromBody]LoginData loginData)
        {
            try
            {
                var token = await _userService.LoginAsync(loginData);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        /// <summary>
        /// Delete user by profile ID.
        /// </summary>
        /// <param name="id">User profile ID.</param>
        /// <returns>404 - user not found; 204 - user deleted.</returns>
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("{id}")]
        public async Task<ActionResult<AppUserDTO>> DeleteUserAsync(int id)
        {
            var userToDelete = await _userService.GetUserProfileAsync(id.ToString());
            if (userToDelete == null)
            {
                return NotFound();
            }
            await _userService.DeleteUserAsync(id);
            return userToDelete;
        }
    }
}