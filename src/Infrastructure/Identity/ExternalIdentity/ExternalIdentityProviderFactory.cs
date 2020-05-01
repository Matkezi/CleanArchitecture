using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity.ExternalIdentity
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
