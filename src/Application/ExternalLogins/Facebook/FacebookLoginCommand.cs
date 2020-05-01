using AutoMapper.Configuration;
using CleanArchitecture.Application.Common.Interfaces;
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
    public class FacebookLoginCommand : IRequest
    {
        public string AuthToken { get; set; }

        public class Handler : IRequestHandler<FacebookLoginCommand>
        {
            private readonly IExternalIdentityService _externalIdentityService;


            public Handler(IExternalIdentityService externalIdentityService)
            {

                _externalIdentityService = externalIdentityService;
            }

            public async Task<Unit> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
            {
                await _externalIdentityService.FacebookLogin(request.AuthToken);
                return Unit.Value;
            }
        }
    }
}
