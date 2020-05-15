using SkipperAgency.Domain.Enums;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IExternalIdentityProviderFactory
    {
        IExternalIdentityProvider GetExternalIdentityProvider(ExternalIdentityProviderEnum externalIdentity);
    }
}
