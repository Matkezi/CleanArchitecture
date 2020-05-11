﻿using System.Threading;
using System.Threading.Tasks;
using System.Web;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.EmailTemplateModels;
using SkipperAgency.Domain.Entities;
using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Skippers.Commands.CreateSkipper
{
    public class CreateSkipperCommand : IRequest
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool GDPRConsentAccepted { get; set; }

        public class Handler : IRequestHandler<CreateSkipperCommand>
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

            public async Task<Unit> Handle(CreateSkipperCommand request, CancellationToken cancellationToken)
            {
                var skipper = new Skipper
                {
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName
                };

                var result = await _identityService.CreateUserAsync(skipper, RoleEnum.Skipper, request.Password);
                if (!result.Result.Succeeded)
                {
                    // TODO: Log, do something...
                    // return something
                }          

                string callbackUrl = $"{_configuration["AppSettings:AppServerUrl"]}/confirm-email?email={skipper.Email}&token={HttpUtility.UrlEncode(result.emailConfirmationToken)}";
 
                await _emailer.SendEmailWithTemplate(
                    new ConfirmEmail(
                        toEmail: skipper.Email,
                        fullName: skipper.FullName,
                        callbackUrl: callbackUrl
                    ));


                await _emailer.SendEmailWithTemplate(
                    new NewSkipperNotice(
                        toEmail: _configuration["AppSettings:MainCharterEmail"],
                        skipperFullName: skipper.FullName,
                        skipperEmail: skipper.Email
                    ));

                return Unit.Value;
            }
        }
    }
}
