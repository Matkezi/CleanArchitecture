using CleanArchitecture.Application.Common.Interfaces;
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
    public class EmailChangeCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string NewEmail { get; set; }
        public string Token { get; set; }        

        public class Handler : IRequestHandler<EmailChangeCommand>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(EmailChangeCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.ChangeEmail(request.UserEmail, request.NewEmail, request.Token);
                return Unit.Value;
            }
        }
    }
}
