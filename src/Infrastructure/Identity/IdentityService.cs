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

namespace CleanArchitecture.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public IdentityService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
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

        public async Task<(Result, LoginResponse)> Login(string email, string password, bool rememberMe)
        {
            AppUser user = await _userManager.FindByNameAsync(email);
            if (user is null) return (Result.Failure("User not foud."), null); 

            // TODO: Uncomment this!
            //var isUserEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);
            //if (!isUserEmailConfirmed)
            //{
            //    string errorMessage = $"Login failed. Email Not Confirmed. User: {email}";
            //    _logger.LogError(errorMessage);
            //    throw new Exception(errorMessage);
            //}

            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                return (result.ToApplicationResult(), null);
            }
            var token = await GetToken(user);
            var roles = await GetUserRoles(user.Email);
            return (result.ToApplicationResult(),
                new LoginResponse 
                { 
                    Token = token, 
                    Username = user.Email, 
                    Role = roles.First().ToString(), 
                    Id = user.Id, 
                    UserPhotoUrl = user.UserPhotoUrl 
                });
        }

        public Task<(Result, LoginResponse)> FacebookLogin(string authToken)
        {
            throw new NotImplementedException();
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

        public Task<IList<RoleEnum>> GetUserRoles(string userName)
        {
            throw new NotImplementedException();
        }

        private async Task<string> GetToken(AppUser user)
        {
            try
            {
                var utcNow = DateTime.UtcNow;
                var userRoles = await _userManager.GetRolesAsync(user);
                var userRolesString = string.Join(", ", userRoles.ToList());

                var claims = new Claim[]
                {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, utcNow.ToString()),
                        new Claim(JwtRegisteredClaimNames.Exp, utcNow.AddSeconds(Convert.ToDouble(_settings.TokenExpiration)).ToString()),
                        new Claim(ClaimTypes.Role, userRolesString),
                        new Claim("UserId", user.Id)
                };

                var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.Secret));
                var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
                var jwt = new JwtSecurityToken(
                    signingCredentials: signingCredentials,
                    claims: claims,
                    notBefore: utcNow,
                    expires: utcNow.AddSeconds(Convert.ToDouble(_settings.TokenExpiration)),
                    audience: _settings.Audience,
                    issuer: _settings.Issuer
                    );

                return new JwtSecurityTokenHandler().WriteToken(jwt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Generate token error. User: {user.Email}");
                throw ex;
            }
        }

    }
}
