using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.Models;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkipperAgency.Infrastructure.Identity
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
            var user1 = await _userManager.FindByEmailAsync(user.Email);

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

        public async Task<Result> ConfirmEmail(string userEmail, string token)
        {
            AppUser user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            return result.ToApplicationResult();
        }

        public async Task<(Result result, LoginResponse loginResponse)> Login(string userEmail, string password, bool rememberMe)
        {
            AppUser user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null) return (Result.Failure("User not found."), null);

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


        public async Task<Result> ChangePassword(string userName, string password, string newPassword)
        {
            AppUser user = await _userManager.FindByNameAsync(userName);
            if (user is null) return Result.Failure("user not found.");

            var result = await _userManager.ChangePasswordAsync(user, password, newPassword);
            if (!result.Succeeded) Result.Failure("Failed to change password.");

            return Result.Success();
        }

        public async Task<(Result result, string passwordResetTokenBase64)> PasswordResetToken(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                return (Result.Failure("User not found."), null);
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenBytes);
            return (Result.Success(), tokenEncoded);
        }

        public async Task<Result> PasswordReset(string userEmail, string newPassword, string token)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                return Result.Failure("User not found.");
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, tokenDecoded, newPassword);
            return result.ToApplicationResult();
        }

        public async Task<(Result result, string emailResetTokenBase64)> ChangeEmailToken(string userEmail, string userNewEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                return (Result.Failure("User not found."), null);
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, userNewEmail);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenBytes);
            return (Result.Success(), tokenEncoded);
        }

        public async Task<Result> ChangeEmail(string userEmail, string userNewEmail, string token)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
                return Result.Failure("User not found.");
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ChangeEmailAsync(user, userNewEmail, tokenDecoded);
            return result.ToApplicationResult();
        }

        public async Task<IList<AppUser>> GetUsersByRole(RoleEnum role)
        {
            return await _userManager.GetUsersInRoleAsync(Enum.GetName(typeof(RoleEnum), role));
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
