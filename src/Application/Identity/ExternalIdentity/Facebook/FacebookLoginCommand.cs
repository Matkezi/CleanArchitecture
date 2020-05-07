using AutoMapper.Configuration;
using CleanArchitecture.Application.Common.Interfaces;
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
    public class FacebookLoginCommand : IRequest<string>
    {
        public string AuthToken { get; set; }

        public class Handler : IRequestHandler<FacebookLoginCommand, string>
        {
            private readonly IExternalIdentityProviderFactory _externalIdentityProviderFactory;


            public Handler(IExternalIdentityProviderFactory externalIdentityProviderFactory)
            {
                _externalIdentityProviderFactory = externalIdentityProviderFactory;
            }

            public async Task<string> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
            {
                var externalIdentityProvider = _externalIdentityProviderFactory.GetExternalIdentityProvider(ExternalIdentityProviderEnum.Facebook);
                var result = await externalIdentityProvider.ExternalLogin(request.AuthToken);
                //TODO what if reulst failed
                return result.loginResponse.Token;
            }
        }
    }
}
