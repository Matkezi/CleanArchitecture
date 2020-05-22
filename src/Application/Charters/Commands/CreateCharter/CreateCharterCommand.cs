using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace SkipperAgency.Application.Charters.Commands.CreateCharter
{
    public class CreateCharterCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Oib { get; set; }
        public string CharterName { get; set; }
        public bool GdprConsentAccepted { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }

        public class Handler : IRequestHandler<CreateCharterCommand>
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

            public async Task<Unit> Handle(CreateCharterCommand request, CancellationToken cancellationToken)
            {
                var charter = new Charter
                {
                    Email = request.Email,
                    Oib = request.Oib,
                    CharterName = request.CharterName,
                    Address = request.Address,
                    City = request.City,
                    ZipCode = request.City,
                    CountryId = request.CountryId
                };

                var emailConfirmationToken = await _identityService.CreateUserAsync(charter, RoleEnum.Charter, request.Password);
                
                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/confirm-email?email={charter.Email}&token={HttpUtility.UrlEncode(emailConfirmationToken)}";

                await _emailService.SendEmailWithTemplate(
                    new ConfirmEmailModel(
                        toEmail: charter.Email,
                        fullName: charter.CharterName,
                        callbackUrl: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
