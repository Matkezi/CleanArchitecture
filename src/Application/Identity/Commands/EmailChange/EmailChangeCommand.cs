using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Application.Identity.Commands.EmailChange
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
