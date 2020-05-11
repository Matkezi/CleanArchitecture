using SkipperAgency.Domain.Entities;
using System.Threading.Tasks;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IJwtServicecs
    {
        Task<string> GenerateEncodedToken(AppUser user);
    }
}
