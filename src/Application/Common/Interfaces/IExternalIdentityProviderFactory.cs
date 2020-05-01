using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IExternalIdentityProviderFactory
    {
        IExternalIdentityProvider GetExternalIdentityProvider(ExternalIdentityProviderEnum externalIdentity);
    }
}
