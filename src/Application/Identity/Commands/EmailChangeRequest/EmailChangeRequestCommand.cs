using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Identity.Commands.EmailChangeRequest
{
    public class EmailChangeRequestCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string UserNewEmail { get; set; }

        public class Handler : IRequestHandler<EmailChangeRequestCommand>
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

            public async Task<Unit> Handle(EmailChangeRequestCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.ChangeEmailToken(request.UserEmail, request.UserNewEmail);
                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/change-email/email={request.UserEmail}/newEmail={request.UserNewEmail}/token={result.emailResetTokenBase64}";

                // TODO: fullname in an email
                _ = _emailer.SendEmailWithTemplate(
                    new ChangeEmail(
                        toEmail: request.UserEmail,
                        fullName: request.UserEmail,
                        changeEmailUrl: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
