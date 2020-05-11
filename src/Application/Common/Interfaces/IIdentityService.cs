using System.Collections.Generic;
using System.Threading.Tasks;
using SkipperAgency.Application.Common.Models;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;

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
        Task<(Result Result, string UserId, string emailConfirmationToken)> CreateUserAsync(AppUser user, RoleEnum role, string password);
        Task<Result> DeleteUserAsync(string userId);
        Task<Result> ConfirmEmail(string userEmail, string token);
        Task<(Result result, LoginResponse loginResponse)> Login(string userEmail, string password, bool rememberMe);
        Task<Result> ChangePassword(string userName, string password, string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Base64 encoded token.</returns>
        Task<(Result result, string passwordResetTokenBase64)> PasswordResetToken(string userEmail);
        Task<Result> PasswordReset(string userEmail, string newPassword, string token);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Base64 encoded token.</returns>
        Task<(Result result, string emailResetTokenBase64)> ChangeEmailToken(string userEmail, string userNewEmail);
        Task<Result> ChangeEmail(string userEmail, string userNewEmail, string token);
        Task<IList<AppUser>> GetUsersByRole(RoleEnum role);
        Task<IList<RoleEnum>> GetUserRoles(string userName);
    }
}
