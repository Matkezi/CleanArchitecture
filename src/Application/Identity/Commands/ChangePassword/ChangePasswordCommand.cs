﻿using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Identity.Commands.ChangePassword
{
    public class ChangePasswordCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }

        public class Handler : IRequestHandler<ChangePasswordCommand>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.ChangePassword(request.UserEmail, request.Password, request.NewPassword);
                return Unit.Value;
            }
        }
    }
}
