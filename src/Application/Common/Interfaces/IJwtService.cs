using System.Threading.Tasks;
using SkipperAgency.Domain.Entities;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IJwtServicecs
    {
        Task<string> GenerateEncodedToken(AppUser user);
    }
}
