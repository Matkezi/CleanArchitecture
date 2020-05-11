using System;
using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Domain.Enums;
using SkipperAgency.Infrastructure.Identity.ExternalIdentity.Facebook;
using SkipperAgency.Infrastructure.Identity.ExternalIdentity.Google;

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
        {
            switch (externalIdentity)
            {
                case ExternalIdentityProviderEnum.Facebook:
                    return (IExternalIdentityProvider)_serviceProvider.GetService(typeof(FacebookIdentityProvider)); ;
                case ExternalIdentityProviderEnum.Google:
                    return (IExternalIdentityProvider)_serviceProvider.GetService(typeof(GoogleIdentityProvider)); ;
                default:
                    throw new ArgumentException("No Supported External Identity."); ; ;
            }

        }

    }
}
