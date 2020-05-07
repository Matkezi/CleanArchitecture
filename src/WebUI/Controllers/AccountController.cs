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
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginModel)
        {
            try
            {
                var loginData = await _identityService.Login(loginModel.Email, loginModel.Password, loginModel.RememberMe);
                if (loginData.loginResponse is null)
                {
                    return Unauthorized();
                }
                return Ok(loginData);
            }
            catch (UnauthorizedAccessException)
            {
                return BadRequest("Email or password aren't correct!");
            }


        }

        [AllowAnonymous]
        [HttpPost("facebook-login")]
        public async Task<ActionResult<string>> FacebookLogin(FacebookLoginCommand command)
        {
            return await Mediator.Send(command);
        }

        [AllowAnonymous]
        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string email, string token)
        {
            var result = await _identityService.ConfirmEmail(email, token);
            if (result.Succeeded) return Ok();
            else return BadRequest(result.Errors.ToList());
        }

        [AllowAnonymous]
        [HttpPost("change-email/{email}/{newEmail}/{token}")]
        public async Task<IActionResult> ChangeEmail(string email, string newEmail, string token)
        {
            var result = await _identityService.ChangeEmail(email, newEmail, token);
            if (result.Succeeded) return Ok();
            else return BadRequest(result.Errors.ToList());
        }

        [AllowAnonymous]
        [HttpPost("email-reset-email/{email}/{newEmail}")]
        public async Task<IActionResult> EmailResetRequest(string email, string newEmail)
        {
            var result = await _identityService.ChangeEmailRequest(email, newEmail);
            if (result.Succeeded) return Ok();
            else return BadRequest(result.Errors.ToList());
        }

        [AllowAnonymous]
        [HttpPost("password-reset/{email}/{token}/{newPassword}")]
        public async Task<IActionResult> PasswordReset(string email, string token, string newPassword)
        {
            var result = await _identityService.PasswordReset(email, token, newPassword);
            if (result.Succeeded) return Ok();
            else return BadRequest(result.Errors.ToList());
        }

        [AllowAnonymous]
        [HttpPost("change-password/{email}/{password}/{newPassword}")]
        public async Task<IActionResult> PasswordResetRequest(string email, string password, string newPassword)
        {
            var result = await _identityService.ChangePassword(email, password, newPassword);
            if (result.Succeeded) return Ok();
            else return BadRequest(result.Errors.ToList());
        }

        [AllowAnonymous]
        [HttpPost("password-reset-email/{email}")]
        public async Task<IActionResult> PasswordResetRequest(string email)
        public async Task<IActionResult> PasswordResetRequest(string email)
        {
            var result = await _identityService.PasswordResetRequest(email);
            if (result.Succeeded) return Ok();
            else return BadRequest(result.Errors.ToList());
        }

    }

}
