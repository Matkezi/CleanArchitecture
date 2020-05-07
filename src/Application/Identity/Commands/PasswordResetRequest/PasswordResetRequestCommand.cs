using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.EmailTemplateModels;
using CleanArchitecture.Domain.Enums;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SkipperBooking.Base.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CleanArchitecture.Application.ExternalLogins.Facebook
{
    public class PasswordResetRequestCommand : IRequest
    {
        public string UserName { get; set; }

        public class Handler : IRequestHandler<PasswordResetRequestCommand>
        {
            private readonly IIdentityService _identityService;
            private readonly IEmailService _emailer;
            private readonly IConfiguration _configuration;

            public Handler(IIdentityService identityService, IEmailService emailer)
            {
                _identityService = identityService;
                _emailer = emailer;
            }

            public async Task<Unit> Handle(PasswordResetRequestCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.PasswordResetToken(request.UserName);
                var email = await _identityService.GetEmailAsync(request.UserName);
                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/password-reset/email={email}/token={result.passwordResetTokenBase64}";

                _ = _emailer.SendEmailWithTemplate(
                    new PasswordReset(
                        toEmail: email,
                        fullName: request.UserName,
                        passwordResetUrl: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
