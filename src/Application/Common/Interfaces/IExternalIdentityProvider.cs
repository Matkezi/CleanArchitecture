using System.Threading.Tasks;
using SkipperAgency.Application.Common.Models;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IExternalIdentityProvider
    {
        Task<(Result result, LoginResponse loginResponse)> ExternalLogin(string authToken);
    }
}
