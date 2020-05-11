using System.Net.Http;
using System.Threading.Tasks;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Infrastructure.HttpClients
{
    public class BasicHttpClient : IHttpClient
    {
        private static readonly HttpClient Client = new HttpClient();
        public async Task<string> GetStringAsync(string url)
        {
            return await Client.GetStringAsync(url);
        }
    }
}
