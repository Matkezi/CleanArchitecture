using CleanArchitecture.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IExternalIdentityProvider
    {
        Task<(Result, LoginResponse)> ExternalLogin(string authToken);
    }
}
