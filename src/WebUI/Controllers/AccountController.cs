using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Application.ExternalLogins.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CleanArchitecture.WebUI.Controllers
{
    public class AccountController : ApiController
    {

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpPost("facebook-login")]
        public async Task<ActionResult<LoginResponse>> FacebookLogin(FacebookLoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<ActionResult> ConfirmEmail(ConfirmEmailCommand command)
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
