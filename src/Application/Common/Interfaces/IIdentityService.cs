using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using SkipperBooking.Base.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IIdentityService
    {
        Task<string> GetEmailAsync(string userName);
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
        Task<Result> ConfirmEmail(string email, string token);
        Task<(Result result, LoginResponse loginResponse)> Login(string userName, string password, bool rememberMe);
        Task<Result> ChangePassword(string userName, string password, string newPassword);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <returns>Base64 encoded token.</returns>
        Task<(Result result, string passwordResetTokenBase64)> PasswordResetToken(string userName);
        Task<Result> PasswordReset(string email, string token, string newPassword);
        Task<Result> ChangeEmailRequest(string userName, string newEmail);
        Task<Result> ChangeEmail(string email, string newEmail, string token);
        Task<IList<AppUser>> GetUsersByRole(RoleEnum role);
        Task<IList<RoleEnum>> GetUserRoles(string userName);
    }
}
