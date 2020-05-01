using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IHttpClient
    {
        Task<string> GetStringAsync(string url);
    }
}
