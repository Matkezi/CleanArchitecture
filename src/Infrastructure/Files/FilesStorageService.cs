using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using SkipperAgency.Application.Common.Interfaces;

namespace SkipperAgency.Infrastructure.Files
{
    public class FilesStorageService : AzureStorageService, IFilesStorageService
    {
        public FilesStorageService(IConfiguration configuration) : 
            base(configuration["AzureStorage:AccessKey"], configuration["AzureStorage:ContainerName"])
        {
        }

        public async Task DeleteCloudAsync(string absoluteUri)
        {
            await DeleteFileAsync(absoluteUri);
        }

        public Task DeleteLocalAsync(string absolutePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> ReplaceCloudAsync(string base64data, string ext, string oldAbsoluteUri)
        {
            var newAbsoluteUri = await SaveCloudAsync(base64data, ext);
            if (!string.IsNullOrEmpty(oldAbsoluteUri))
            {
                await DeleteCloudAsync(oldAbsoluteUri);
            }
            return newAbsoluteUri;
        }

        public async Task<string> SaveCloudAsync(byte[] byteArray, string ext)
        {
            return await UploadFileAsync(byteArray, ext);
        }

        public async Task<string> SaveCloudAsync(string base64data, string ext)
        {
            var byteArray = Convert.FromBase64String(base64data);
            return await SaveCloudAsync(byteArray, ext);
        }

        public Task<string> SaveLocalAsync(byte[] byteArray, string name, string ext)
        {
            throw new NotImplementedException();
        }

        public Task<string> SaveLocalAsync(string base64data, string name, string ext)
        {
            throw new NotImplementedException();
        }
    }
}
