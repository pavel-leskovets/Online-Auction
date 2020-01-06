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
        /// Get all users.
        /// </summary>
        /// <returns>200 users found.</returns>
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult<IEnumerable<AppUserDTO>> GetUsers()
        {
            var users = _userService.GetUsers();
            return Ok(users);
        }

        /// <summary>
        /// Get user by profile.
        /// </summary>
        /// <returns>200 - user found; 404 - user not found.</returns>
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

        /// <summary>
        /// Get user by profile ID.
        /// </summary>
        /// <param name="id">User profile ID.</param>
        /// <returns>200 - user found; 404 - user not found.</returns>
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


        /// <summary>
        /// Update user profile.
        /// </summary>
        /// <param name="user">User.</param>
        /// <param name="id">User ID.</param>
        /// <returns>204 - user updated; 404 - user not found; 400 - validation failed.</returns>
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
            catch (Exception ex)
            {
                if (_userService.GetUserProfileAsync(id.ToString()) == null)
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
        /// Get lots by user profile.
        /// </summary>
        /// <returns>200 lots found; 404 - user not found.</returns>
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
            return Ok(lots);
        }


        /// <summary>
        /// Get bids by user profile.
        /// </summary>
        /// <returns>200 - bids found; 404 - user not found.</returns>
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
            return Ok(bids);
        }

        /// <summary>
        /// Register and create new user.
        /// </summary>
        /// <param name="regData">Registration model.</param>
        /// <returns>201 - user created, 400 - validation failed.</returns>
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

        /// <summary>
        /// Authorize user.
        /// </summary>
        /// <param name="loginData">User login model.</param>
        /// <returns></returns>
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
        /// <returns>404 - user not found; 200 - user deleted.</returns>
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