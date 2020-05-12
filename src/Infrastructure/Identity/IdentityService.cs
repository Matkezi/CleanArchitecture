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
using SkipperAgency.Application.Common.Exceptions;

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
        /// Creates a new user where: Username == Email.
        /// </summary>
        /// <param name="newUser"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<string> CreateUserAsync(AppUser newUser, RoleEnum role, string password)
        {
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            if (user != null)
            {
                throw new UniqueConstraintException("User email", newUser.Email);
            }

            newUser.UserName = newUser.Email;
            var result = await _userManager.CreateAsync(newUser, password);
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't create user ({string.Join(";", result.Errors)})");
            }
            result = await _userManager.AddToRoleAsync(newUser, Enum.GetName(typeof(RoleEnum), role));
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't add roles to user ({string.Join(";", result.Errors)})");
            }

            return await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
        }

        public async Task DeleteUserAsync(string userId)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

            if (user != null)
            {
                await DeleteUserAsync(user);
            }
        }

        public async Task DeleteUserAsync(AppUser user)
        {
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                throw new Exception($"Couldn't delete user {user.UserName} ({string.Join(";", result.Errors)})");
            }
        }

        public async Task ConfirmEmail(string userEmail, string token)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded)
            {
                // TODO: try parsing result a bit better, like if token is invalid
                throw new ConfirmEmailException(userEmail,string.Join(";", result.Errors) );
            }
        }

        public async Task<LoginResponse> Login(string userEmail, string password, bool rememberMe)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new NotFoundException("User", userEmail);
            }

            if (!await _userManager.CheckPasswordAsync(user, password))
                throw new UnauthorizedAccessException("Invalid password.");

            var isUserEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            if (!isUserEmailConfirmed)
            {
                throw new ConfirmEmailException(user.Email, "Verification email sent. Please check your email.");
            }
            var token = await _jwtFactory.GenerateEncodedToken(user);
            var roles = await GetUserRoles(user.UserName);
            return (
                new LoginResponse
                {
                    Token = token,
                    Username = user.UserName,
                    Role = roles.First().ToString(),
                    Id = user.Id,
                    UserPhotoUrl = user.UserPhotoUrl
                });

        }


        public async Task ChangePassword(string userEmail, string password, string newPassword)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new NotFoundException("User", userEmail);
            }

            var result = await _userManager.ChangePasswordAsync(user, password, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to change password ({string.Join(",", result.Errors)})");
            }
        }

        public async Task<string> PasswordResetToken(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new NotFoundException("User", userEmail);
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenBytes);
            return tokenEncoded;
        }

        public async Task PasswordReset(string userEmail, string newPassword, string token)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new NotFoundException("User", userEmail);
            }
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ResetPasswordAsync(user, tokenDecoded, newPassword);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to reset password ({string.Join(",", result.Errors)})");
            }
        }

        public async Task<string> ChangeEmailToken(string userEmail, string userNewEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new NotFoundException("User", userEmail);
            }
            var token = await _userManager.GenerateChangeEmailTokenAsync(user, userNewEmail);
            byte[] tokenBytes = Encoding.UTF8.GetBytes(token);
            var tokenEncoded = WebEncoders.Base64UrlEncode(tokenBytes);
            return tokenEncoded;
        }

        public async Task ChangeEmail(string userEmail, string userNewEmail, string token)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new NotFoundException(nameof(AppUser), userEmail);
            }
            var tokenDecodedBytes = WebEncoders.Base64UrlDecode(token);
            var tokenDecoded = Encoding.UTF8.GetString(tokenDecodedBytes);
            var result = await _userManager.ChangeEmailAsync(user, userNewEmail, tokenDecoded);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to change email ({string.Join(",", result.Errors)})");
            }
        }

        public async Task<IList<AppUser>> GetUsersByRole(RoleEnum role)
        {
            return await _userManager.GetUsersInRoleAsync(Enum.GetName(typeof(RoleEnum), role));
        }

        public async Task<IList<RoleEnum>> GetUserRoles(string userEmail)
        {
            var user = await _userManager.FindByEmailAsync(userEmail);
            if (user is null)
            {
                throw new NotFoundException(nameof(AppUser), userEmail);
            }
            var roles = await _userManager.GetRolesAsync(user);

            return roles.Select(role =>
            {
                Enum.TryParse(role, out RoleEnum roleEnum);
                return roleEnum;
            }).ToList();
        }
    }
}
