using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Identity.Commands.PasswordResetRequest
{
    public class PasswordResetRequestCommand : IRequest
    {
        public string UserEmail { get; set; }

        public class Handler : IRequestHandler<PasswordResetRequestCommand>
        {
            private readonly IIdentityService _identityService;
            private readonly IEmailService _emailer;
            private readonly IConfiguration _configuration;

            public Handler(IIdentityService identityService, IEmailService emailer, IConfiguration configuration)
            {
                _identityService = identityService;
                _emailer = emailer;
                _configuration = configuration;
            }

            public async Task<Unit> Handle(PasswordResetRequestCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.PasswordResetToken(request.UserEmail);
                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/password-reset/email={request.UserEmail}/token={result.passwordResetTokenBase64}";

                // TODO: fullname in an email
                _ = _emailer.SendEmailWithTemplate(
                    new Domain.EmailTemplateModels.PasswordReset(
                        toEmail: request.UserEmail,
                        fullName: request.UserEmail,
                        passwordResetUrl: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
