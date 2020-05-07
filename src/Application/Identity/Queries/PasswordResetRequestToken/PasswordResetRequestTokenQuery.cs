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
    public class PasswordResetRequestTokenQuery : IRequest<string>
    {
        public string UserName { get; set; }

        public class Handler : IRequestHandler<PasswordResetRequestTokenQuery, string>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<string> Handle(PasswordResetRequestTokenQuery request, CancellationToken cancellationToken)
            {
                var result = await _identityService.PasswordResetToken(request.UserName);
                return result.passwordResetTokenBase64;
            }
        }
    }
}
