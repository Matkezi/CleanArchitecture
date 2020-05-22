using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SkipperAgency.Application.Skippers.Commands.CreateSkipper
{
    public class CreateSkipperCommand : IRequest
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool GdprConsentAccepted { get; set; }

        public class Handler : IRequestHandler<CreateSkipperCommand>
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

            public async Task<Unit> Handle(CreateSkipperCommand request, CancellationToken cancellationToken)
            {
                var skipper = new Skipper
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };

                var emailConfirmationToken = await _identityService.CreateUserAsync(skipper, RoleEnum.Skipper, request.Password);


                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/confirm-email?email={skipper.Email}&token={HttpUtility.UrlEncode(emailConfirmationToken)}";

                await _emailService.SendEmailWithTemplate(
                    new ConfirmEmailModel(
                        toEmail: skipper.Email,
                        fullName: skipper.FullName,
                        callbackUrl: callbackUrl
                    ));


                _ =_emailService.SendEmailWithTemplate(
                    new NewSkipperNoticeModel(
                        toEmail: _configuration["AppSettings:MainCharterEmail"],
                        skipperFullName: skipper.FullName,
                        skipperEmail: skipper.Email
                    ));

                return Unit.Value;
            }
        }
    }
}
