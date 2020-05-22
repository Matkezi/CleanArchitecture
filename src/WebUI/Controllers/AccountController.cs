using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Common.Models;
using SkipperAgency.Application.Identity.Commands.ChangePassword;
using SkipperAgency.Application.Identity.Commands.ConfirmEmail;
using SkipperAgency.Application.Identity.Commands.EmailChange;
using SkipperAgency.Application.Identity.Commands.EmailChangeRequest;
using SkipperAgency.Application.Identity.Commands.Login;
using SkipperAgency.Application.Identity.Commands.PasswordReset;
using SkipperAgency.Application.Identity.Commands.PasswordResetRequest;
using SkipperAgency.Application.Identity.ExternalIdentity.Facebook;
using System.Threading.Tasks;

namespace SkipperAgency.WebUI.Controllers
{
    [Authorize]
    public class AccountController : ApiController
    {
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<LoginResponse> Login(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("facebook-login")]
        [AllowAnonymous]
        public async Task<LoginResponse> FacebookLogin(FacebookLoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("confirm-email")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        
        [HttpPost("change-email/{email}/{newEmail}/{token}")]
        public async Task<IActionResult> ChangeEmail(EmailChangeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("email-reset-email/{email}/{newEmail}")]
        public async Task<IActionResult> EmailResetRequest(EmailChangeRequestCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("password-reset/{email}/{token}/{newPassword}")]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordReset(PasswordResetCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("change-password")]
        [Authorize(Roles = "Admin, Skipper, Charter")]
        public async Task<IActionResult> ChangePassword(ChangePasswordCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("password-reset-email/{email}")]
        [AllowAnonymous]
        public async Task<IActionResult> PasswordResetRequest(PasswordResetRequestCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

    }

}
