using BLL.Infrastructure;
using BLL.ModelsDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    /// <summary>
    /// Interface for users services.
    /// Contains methods for managing users.
    /// </summary>
    public interface IUserService : IDisposable
    {
        /// <summary>
        /// Method that gets all users.
        /// </summary>
        IEnumerable<AppUserDTO> GetUsers();

        /// <summary>
        /// Async method for creating new user with password.
        /// </summary>
        /// <param name="regData">User registration entity</param>
        Task<AppUserDTO> CreateUserAsync(UserRegistration regData);

        /// <summary>
        /// Async method for authenticating user.
        /// </summary>
        /// <param name="loginData">Model for user authentication</param>
        /// <returns>The Task, containing token string.</returns>
        Task<string> LoginAsync(LoginData loginData);

        /// <summary>
        /// Async method for finding user by profile ID.
        /// </summary>
        /// <param name="userId">The profile ID.</param>
        /// <returns>The Task, containing user DTO.</returns>
        Task<AppUserDTO> GetUserProfileAsync(string userId);

        /// <summary>
        /// Method for finding bids by profile ID.
        /// </summary>
        /// <param name="userId">The profile ID.</param>
        /// <returns>Collection of bids DTOs.</returns>
        IEnumerable<BidDTO> GetBidsByProfile(int userId);

        /// <summary>
        /// Method for finding lots by profile ID.
        /// </summary>
        /// <param name="userId">The profile ID.</param>
        /// <returns>Collection of lots DTOs.</returns>
        IEnumerable<LotDTO> GetLotsByProfile(int userId);

        /// <summary>
        /// Async method for update user profile.
        /// </summary>
        /// <param name="user">User DTO.</param>
        /// <returns>The Task.</returns>
        Task UpdateProfileAsync(AppUserDTO user);

        /// <summary>
        /// Async method for deleting user. 
        /// </summary>
        /// <param name="id">The profile ID.</param>
        /// <returns>The Task.</returns>
        Task DeleteUserAsync(int id);
    }
}
