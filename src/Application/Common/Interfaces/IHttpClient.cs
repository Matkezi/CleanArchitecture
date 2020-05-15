using System.Threading.Tasks;

namespace SkipperAgency.Application.Common.Interfaces
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string url);
    }
}
