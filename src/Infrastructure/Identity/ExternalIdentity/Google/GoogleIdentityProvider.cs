using SkipperAgency.Application.Common.Interfaces;
using SkipperAgency.Application.Common.Models;
using System;
using System.Threading.Tasks;

namespace SkipperAgency.Infrastructure.Identity.ExternalIdentity.Google
{
    public class GoogleIdentityProvider : IExternalIdentityProvider
    {
        public Task<(Result result, LoginResponse loginResponse)> ExternalLogin(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}
