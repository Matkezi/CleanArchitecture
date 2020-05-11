using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkipperAgency.Application.Common.Models;
using SkipperAgency.Application.Identity.Commands.ConfirmEmail;
using SkipperAgency.Application.Identity.Commands.EmailChange;
using SkipperAgency.Application.Identity.Commands.EmailChangeRequest;
using SkipperAgency.Application.Identity.Commands.Login;
using SkipperAgency.Application.Identity.Commands.PasswordReset;
using SkipperAgency.Application.Identity.Commands.PasswordResetRequest;
using SkipperAgency.Application.Identity.ExternalIdentity.Facebook;

namespace SkipperAgency.WebUI.Controllers
{
    public class AccountController : ApiController
    {

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<LoginResponse> Login(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("facebook-login")]
        public async Task<LoginResponse> FacebookLogin(FacebookLoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("change-email/{email}/{newEmail}/{token}")]
        public async Task<IActionResult> ChangeEmail(EmailChangeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("email-reset-email/{email}/{newEmail}")]
        public async Task<IActionResult> EmailResetRequest(EmailChangeRequestCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("password-reset/{email}/{token}/{newPassword}")]
        public async Task<IActionResult> PasswordReset(PasswordResetCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [AllowAnonymous]
        [HttpPost("password-reset-email/{email}")]
        public async Task<IActionResult> PasswordResetRequest(PasswordResetRequestCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

    }

}
