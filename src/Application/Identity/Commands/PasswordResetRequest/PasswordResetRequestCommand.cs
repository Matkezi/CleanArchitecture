using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Identity.Commands.PasswordResetRequest
{
    public class PasswordResetRequestCommand : IRequest
    {
        public string UserEmail { get; set; }

        public class Handler : IRequestHandler<PasswordResetRequestCommand>
        {
            private readonly IEmailService _emailService;
            private readonly IIdentityService _identityService;
            private readonly IConfiguration _configuration;

            public Handler(IEmailService emailService, IIdentityService identityService, IConfiguration configuration)
            {
                _emailService = emailService;
                _identityService = identityService;
                _configuration = configuration;
            }

            public async Task<Unit> Handle(PasswordResetRequestCommand request, CancellationToken cancellationToken)
            {
                var passwordResetTokenBase64 = await _identityService.PasswordResetToken(request.UserEmail);
                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/password-reset/email={request.UserEmail}/token={passwordResetTokenBase64}";

                // TODO: fullname in an email
                _ = _emailService.SendEmailWithTemplate(
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
