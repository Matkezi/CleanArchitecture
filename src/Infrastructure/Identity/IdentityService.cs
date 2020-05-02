using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using System.Linq;
using System.Threading.Tasks;
using SkipperBooking.Base.Enums;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Newtonsoft.Json;

namespace CleanArchitecture.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtServicecs _jwtFactory;

        public IdentityService(UserManager<AppUser> userManager, IJwtServicecs jwtFactory)
        {
            _userManager = userManager;
            _jwtFactory = jwtFactory;
        }

        public async Task<string> GetUserNameAsync(string userId)
        {
            var user = await _userManager.Users.FirstAsync(u => u.Id == userId);

            return user.UserName;
        }

        /// <summary>
        /// Creates a new user where: Username = Email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<(Result Result, string UserId, string emailConfirmationToken)> CreateUserAsync(AppUser user, RoleEnum role, string password)
        {
            user.UserName = user.Email;
            var result = await _userManager.CreateAsync(user, password);
            if (!result.Succeeded)
            {
                return (result.ToApplicationResult(), user.Id, null);
            }
            result = await _userManager.AddToRoleAsync(user, Enum.GetName(typeof(RoleEnum), role));
            if (!result.Succeeded)
            {
                return (result.ToApplicationResult(), user.Id, null);
            }

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            return (result.ToApplicationResult(), user.Id, token);
        }

        public async Task<Result> DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                return await DeleteUserAsync(user);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);

            return result.ToApplicationResult();
        }

        public async Task<Result> ConfirmEmail(string email, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(email);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.ToApplicationResult();
        }

        public async Task<(Result result, LoginResponse loginResponse)> Login(string userName, string password, bool rememberMe)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user is null) return (Result.Failure("User not foud."), null);

            // get the user to verifty
            //var userToVerify = await _userManager.FindByNameAsync(userName);

            //if (userToVerify == null) return await Task.FromResult<ClaimsIdentity>(null);

            // check the credentials
            if (await _userManager.CheckPasswordAsync(user, password))
            {
                // TODO: Uncomment this!
                //var isUserEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
                //if (!isUserEmailConfirmed)
                //{
                //    string errorMessage = $"Login failed. Email Not Confirmed. User: {email}";
                //    _logger.LogError(errorMessage);
                //    throw new Exception(errorMessage);
                //}

                var token = await _jwtFactory.GenerateEncodedToken(user);
                var roles = await GetUserRoles(user.UserName);
                return (Result.Success(),
                    new LoginResponse
                    {
                        Token = token,
                        Username = user.UserName,
                        Role = roles.First().ToString(),
                        Id = user.Id,
                        UserPhotoUrl = user.UserPhotoUrl
                    });
                //return await Task.FromResult(_jwtFactory.GenerateClaimsIdentity(userName, userToVerify.Id));
            }

            return (Result.Failure("Wrong credentials"), null);
        }


        public Task<Result> ChangePassword(string userName, string newPassword, string token)
        {
            throw new NotImplementedException();
        }

        public Task<Result> PasswordResetRequest(string userName)
        {
            throw new NotImplementedException();
        }

        public Task<Result> PasswordReset(string email, string token, string newPassword)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ChangeEmailRequest(string userName, string newEmail)
        {
            throw new NotImplementedException();
        }

        public Task<Result> ChangeEmail(string email, string newEmail, string token)
        {
            throw new NotImplementedException();
        }

        public Task<IList<AppUser>> GetUsersByRole(RoleEnum role)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<RoleEnum>> GetUserRoles(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            // Get the roles for the user
            var roles = await _userManager.GetRolesAsync(user);

            return roles.Select(role =>
            {
                Enum.TryParse(role, out RoleEnum roleEnum);
                return roleEnum;
            }).ToList();
        }
    }
}
