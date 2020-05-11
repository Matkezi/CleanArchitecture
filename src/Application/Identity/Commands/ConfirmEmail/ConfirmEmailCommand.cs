using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Identity.Commands.ConfirmEmail
{
    public class ConfirmEmailCommand : IRequest
    {
        public string UserEmail { get; set; }
        public string Token { get; set; }

        public class Handler : IRequestHandler<ConfirmEmailCommand>
        {
            private readonly IIdentityService _identityService;

            public Handler(IIdentityService identityService)
            {
                _identityService = identityService;
            }

            public async Task<Unit> Handle(ConfirmEmailCommand request, CancellationToken cancellationToken)
            {
                var result = await _identityService.ConfirmEmail(request.UserEmail, request.Token);
                return Unit.Value;
            }
        }
    }
}
