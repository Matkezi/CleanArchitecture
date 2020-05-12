using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Identity.Commands.PasswordReset
{
    public class PasswordResetCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }

        public class Handler : IRequestHandler<PasswordResetCommand>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(PasswordResetCommand request, CancellationToken cancellationToken)
            {
                await _identityService.PasswordReset(request.UserEmail, request.NewPassword, request.Token);
                return Unit.Value;
            }
        }
    }
}
