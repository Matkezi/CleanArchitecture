using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Enums;
using SkipperAgency.Infrastructure.Identity.ExternalIdentity.Facebook;
using SkipperAgency.Infrastructure.Identity.ExternalIdentity.Google;
using System;

namespace SkipperAgency.Infrastructure.Identity.ExternalIdentity
{
    public class ExternalIdentityProviderFactory : IExternalIdentityProviderFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ExternalIdentityProviderFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IExternalIdentityProvider GetExternalIdentityProvider(ExternalIdentityProviderEnum externalIdentity) 
            => externalIdentity switch
            {
                ExternalIdentityProviderEnum.Facebook => (IExternalIdentityProvider) _serviceProvider.GetService(
                    typeof(FacebookIdentityProvider)),
                ExternalIdentityProviderEnum.Google => (IExternalIdentityProvider) _serviceProvider.GetService(
                    typeof(GoogleIdentityProvider)),
                _ => throw new ArgumentOutOfRangeException(nameof(externalIdentity), externalIdentity, "Not Supported External Identity.")
            };
    }
}
