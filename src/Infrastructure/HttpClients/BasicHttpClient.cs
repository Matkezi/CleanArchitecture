using CleanArchitecture.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.HttpClients
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
