﻿using AutoMapper;
using BLL.ModelsDTO;
using DAL.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.Infrastructure;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Options;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using BLL.Exeptions;

namespace BLL.Services
{
    /// <summary>
    /// Contains methods for managing users and their profiles.
    /// </summary>
    public class UserService : IUserService
    {
        private IUnitOfWork _uow;

        private IMapper _mapper;

        private readonly JwtSettings _jwtSettings = new JwtSettings();

        public UserService(IUnitOfWork uow, IMapper mapper, IOptions<JwtSettings> jwtSettings)
        {
            _uow = uow;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
        }

        /// <summary>
        /// Method that gets all users.
        /// </summary>
        public IEnumerable<AppUserDTO> GetUsers()
        {
            var mapped = _mapper.Map<IEnumerable<AppUserDTO>>(_uow.Users.GetAll());
            return mapped;
        }

        /// <summary>
        /// Async method for creating new user with password.
        /// </summary>
        /// <param name="regData">User registration entity</param>
        /// <exception cref="ArgumentNullException">Thrown if user is null.</exception>
        /// <exception cref="ValidationException">Thrown if user validation failed.</exception>
        public async Task<IdentityResult> CreateUserAsync(UserRegistration regData)
        {
            if (regData == null)
                throw new ArgumentNullException(nameof(regData), "User is null.");
            if (await _uow.UserManager.FindByNameAsync(regData.UserName) != null)
                throw new ValidationException("User with this name already exists.");
            if (await _uow.UserManager.FindByEmailAsync(regData.Email) != null)
                throw new ValidationException("User with this email already exists.");
            var appUser = new AppUser
            {
                UserName = regData.UserName,
                Email = regData.Email,
            };
            var result = await _uow.UserManager.CreateAsync(appUser, regData.Password);
            await _uow.UserManager.AddToRoleAsync(appUser, "Customer");
            return result;
        }

        /// <summary>
        /// Async method for authenticating user.
        /// </summary>
        /// <param name="loginData">Entity for user authentication</param>
        /// <returns>The Task, containing token string.</returns>
        public async Task<string> LoginAsync(LoginData loginData)
        {
            var user = await _uow.UserManager.FindByNameAsync(loginData.UserName);
            if (user != null && await _uow.UserManager.CheckPasswordAsync(user, loginData.Password))
            {
                var role = await _uow.UserManager.GetRolesAsync(user);
                IdentityOptions options = new IdentityOptions();
                var securityTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim("UserId", user.Id.ToString()),
                        new Claim(options.ClaimsIdentity.RoleClaimType, role.FirstOrDefault())
                    }),
                    Expires = DateTime.UtcNow.Add(_jwtSettings.LifeTime),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.JwtKey)), SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(securityTokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);
                return token;
            }
            else
            {
                throw new ValidationException("User name or pasword is incorrect");
            }
        }

        /// <summary>
        /// Async method for finding user by profile ID.
        /// </summary>
        /// <param name="userId">The profile ID.</param>
        /// <returns>The Task, containing user DTO.</returns>
        /// <exception cref="NotFoundException">Thrown if user not found in DB.</exception>
        public async Task<AppUserDTO> GetUserProfileAsync(string userId)
        {
            var user = await _uow.UserManager.FindByIdAsync(userId);
            var mapped = _mapper.Map<AppUserDTO>(user);
            return mapped;
        }


        public IEnumerable<LotDTO> GetLotsByProfileAsync(string userId)
        {
            var lots = _uow.Lots.Find(x => x.UserId.Equals(userId));
           
            return _mapper.Map<IEnumerable<LotDTO>>(lots);
        }

        public IEnumerable<BidDTO> GetBidsByProfile(int userId)
        {
            var mapped = _mapper.Map<IEnumerable<BidDTO>>(_uow.Bids.Find(x => x.UserId.Equals(userId)));
            return mapped;
        }

        public async Task UpdateProfileAsync(AppUserDTO user)
        {
            var userToUpdate =  await _uow.UserManager.FindByIdAsync(user.Id.ToString());
            userToUpdate.Email = user.Email;
            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.Addres = user.Addres;
            userToUpdate.Phone = user.Phone;
            await _uow.UserManager.UpdateAsync(userToUpdate);
        }

        public void Dispose()
        {
            _uow.Dispose();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _uow.UserManager.FindByIdAsync(id.ToString());
            await _uow.UserManager.DeleteAsync(user);
        }
    }
}
