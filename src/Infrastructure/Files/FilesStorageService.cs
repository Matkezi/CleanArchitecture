using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Files
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
