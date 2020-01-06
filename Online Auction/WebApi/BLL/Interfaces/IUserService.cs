using BLL.Infrastructure;
using BLL.ModelsDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public interface IUserService : IDisposable
    {
        IEnumerable<AppUserDTO> GetUsers();

        Task<IdentityResult> CreateUserAsync(UserRegistration regData);
        
        Task<string> LoginAsync(LoginData loginData);

        Task<AppUserDTO> GetUserProfileAsync(string userId);

        IEnumerable<BidDTO> GetBidsByProfile(int userId);

        IEnumerable<LotDTO> GetLotsByProfileAsync(string userId);

        Task UpdateProfileAsync(AppUserDTO user);

        Task DeleteUserAsync(int id);
    }
}
