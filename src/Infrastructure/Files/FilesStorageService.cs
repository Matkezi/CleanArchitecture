using CleanArchitecture.Application.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Files
{
    public class FilesStorageService : IFilesStorageService
    {
        private readonly ICloudStorageService _cloudStorageService;

        public FilesStorageService(ICloudStorageService cloudStorageService)
        {
            _cloudStorageService = cloudStorageService;
        }

        public async Task DeleteCloudAsync(string absoluteUri)
        {
            await _cloudStorageService.DeleteFileAsync(absoluteUri);
        }

        public Task DeleteLocalAsync(string absolutePath)
        {
            throw new NotImplementedException();
        }

        public async Task<string> SaveCloudAsync(byte[] byteArray, string ext)
        {
            return await _cloudStorageService.UploadFileAsync(byteArray, ext);
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
