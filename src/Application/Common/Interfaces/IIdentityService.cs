using SkipperAgency.Application.Common.Models;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetUserNameAsync(string userId);
        /// <summary>
        /// Creates a new user where: Username = Email
        /// </summary>
        /// <param name="user"></param>
        /// <param name="role"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<string> CreateUserAsync(AppUser newUser, RoleEnum role, string password);
        Task DeleteUserAsync(string userId);
        Task ConfirmEmail(string userEmail, string token);
        Task<LoginResponse> Login(string userEmail, string password, bool rememberMe);
        Task ChangePassword(string userEmail, string password, string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns>Base64 encoded token.></returns>
        Task<string> PasswordResetToken(string userEmail);
        Task PasswordReset(string userEmail, string newPassword, string token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userEmail"></param>
        /// <param name="userNewEmail"></param>
        /// <returns>Base64 encoded token.</returns>
        Task<string> ChangeEmailToken(string userEmail, string userNewEmail);
        Task ChangeEmail(string userEmail, string userNewEmail, string token);
        Task<IList<AppUser>> GetUsersByRole(RoleEnum role);
        Task<IList<RoleEnum>> GetUserRoles(string userEmail);
    }
}
