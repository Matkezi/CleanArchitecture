using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.Models;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Identity.Commands.Login
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
