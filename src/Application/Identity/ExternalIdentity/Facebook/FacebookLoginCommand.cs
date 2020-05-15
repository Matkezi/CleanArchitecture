using MediatR;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.Models;
using SkipperAgency.Domain.Enums;
using System.Threading;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Identity.ExternalIdentity.Facebook
{
    public class FacebookLoginCommand : IRequest<LoginResponse>
    {
        public string AuthToken { get; set; }

        public class Handler : IRequestHandler<FacebookLoginCommand, LoginResponse>
        {
            private readonly IExternalIdentityProviderFactory _externalIdentityProviderFactory;


            public Handler(IExternalIdentityProviderFactory externalIdentityProviderFactory)
            {
                _externalIdentityProviderFactory = externalIdentityProviderFactory;
            }

            public async Task<LoginResponse> Handle(FacebookLoginCommand request, CancellationToken cancellationToken)
            {
                var externalIdentityProvider = _externalIdentityProviderFactory.GetExternalIdentityProvider(ExternalIdentityProviderEnum.Facebook);
                return await externalIdentityProvider.ExternalLogin(request.AuthToken);
            }
        }
    }
}
