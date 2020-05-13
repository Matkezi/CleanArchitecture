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
using System.Threading.Tasks;

namespace SkipperAgency.WebUI.Controllers
{
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        [HttpPost("login")]
        public async Task<LoginResponse> Login(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("facebook-login")]
        public async Task<LoginResponse> FacebookLogin(FacebookLoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(ConfirmEmailCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpPost("change-email/{email}/{newEmail}/{token}")]
        public async Task<IActionResult> ChangeEmail(EmailChangeCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [Authorize]
        [HttpPost("email-reset-email/{email}/{newEmail}")]
        public async Task<IActionResult> EmailResetRequest(EmailChangeRequestCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("password-reset/{email}/{token}/{newPassword}")]
        public async Task<IActionResult> PasswordReset(PasswordResetCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

        [HttpPost("password-reset-email/{email}")]
        public async Task<IActionResult> PasswordResetRequest(PasswordResetRequestCommand command)
        {
            await Mediator.Send(command);
            return NoContent();
        }

    }

}
