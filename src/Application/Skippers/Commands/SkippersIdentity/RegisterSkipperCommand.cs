using AutoMapper.Configuration;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Persistence.Entities;
using MediatR;
using Microsoft.Extensions.Configuration;
using SkipperBooking.Base.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace CleanArchitecture.Application.Skippers.Commands.SkippersIdentity
{
    public class RegisterSkipperCommand : IRequest
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool GDPRConsentAccepted { get; set; }

        public class Handler : IRequestHandler<RegisterSkipperCommand>
        {
            private readonly IEmailService _emailer;
            private readonly IIdentityService _identityService;
            private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

            public Handler(IEmailService emailer, IIdentityService identityService)
            {

                _emailer = emailer;
                _identityService = identityService;
            }

            public async Task<Unit> Handle(RegisterSkipperCommand request, CancellationToken cancellationToken)
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
