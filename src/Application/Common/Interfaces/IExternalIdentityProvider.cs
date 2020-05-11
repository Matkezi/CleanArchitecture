using SkipperAgency.Application.Common.Models;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IExternalIdentityProvider
    {
        Task<(Result result, LoginResponse loginResponse)> ExternalLogin(string authToken);
    }
}
