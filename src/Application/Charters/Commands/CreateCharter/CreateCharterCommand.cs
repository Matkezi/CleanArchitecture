using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Emails;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperBooking.Base.Enums;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CleanArchitecture.Application.Skippers.Commands.SkippersIdentity
{
    public class UpdateCharterCommand : IRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Oib { get; set; }
        public string CharterName { get; set; }
        public bool GDPRConsentAccepted { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public int CountryId { get; set; }

        public class Handler : IRequestHandler<UpdateCharterCommand>
        {
            private readonly IEmailService _emailer;
            private readonly IIdentityService _identityService;
            private readonly IConfiguration _configuration;

            public Handler(IEmailService emailer, IIdentityService identityService, IConfiguration configuration)
            {
                _emailer = emailer;
                _identityService = identityService;
                _configuration = configuration;
            }

            public async Task<Unit> Handle(UpdateCharterCommand request, CancellationToken cancellationToken)
            {
                var charter = new Charter
                {
                    Email = request.Email,
                    OIB = request.Oib,
                    CharterName = request.CharterName,
                    Address = request.Address,
                    City = request.City,
                    ZipCode = request.City,
                    CountryId = request.CountryId
                };

                var result = await _identityService.CreateUserAsync(charter, RoleEnum.Charter, request.Password);
                if (!result.Result.Succeeded)
                {
                    // TODO: Log, do something...
                    // return something
                }

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/confirm-email?email={charter.Email}&token={HttpUtility.UrlEncode(result.emailConfirmationToken)}";

                await _emailer.SendEmailWithTemplate(
                    new ConfirmEmail(
                        toEmail: charter.Email,
                        fullName: charter.CharterName,
                        callbackUrl: callbackUrl
                    ));

                return Unit.Value;
            }
        }
    }
}
