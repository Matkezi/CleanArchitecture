using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using CleanArchitecture.Domain.EmailTemplateModels;
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
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; }

        public class Handler : IRequestHandler<LoginCommand, LoginResponse>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.Login(request.Email, request.Password, request.RememberMe);
                return result.loginResponse;
            }
        }
    }
}
