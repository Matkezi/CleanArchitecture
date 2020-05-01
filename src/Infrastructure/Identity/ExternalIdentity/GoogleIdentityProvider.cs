using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Identity.ExternalIdentity
{
    public class GoogleIdentityProvider : IExternalIdentityProvider
    {
        public Task<(Result result, LoginResponse loginResponse)> ExternalLogin(string authToken)
        {
            throw new NotImplementedException();
        }
    }
}
