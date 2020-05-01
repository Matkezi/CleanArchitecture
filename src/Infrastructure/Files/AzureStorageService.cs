using CleanArchitecture.Application.Common.Helpers;
using CleanArchitecture.Application.Common.Interfaces;
using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Files
{
    public class AzureStorageService : ICloudStorageService
    {
        private CloudBlobContainer _blobContainer;

        public AzureStorageService(IConfiguration configuration)
        {
            Init(configuration["AzureStorage:AccessKey"], configuration["AzureStorage:ContainerName"]);
        }
        private async void Init(string accessKey, string containerName)
        {
            if (string.IsNullOrEmpty(containerName))
            {
                throw new ArgumentNullException("ContainerName", "Container Name can't be empty");
            }

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(accessKey);

            CloudBlobClient cloudBlobClient = storageAccount.CreateCloudBlobClient();
            _blobContainer = cloudBlobClient.GetContainerReference(containerName);

            if (await _blobContainer.CreateIfNotExistsAsync())
            {
                await _blobContainer.SetPermissionsAsync(
                    new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    }
                );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64data"></param>
        /// <param name="ext"></param>
        /// <returns>Absolute URL.</returns>
        public async Task<string> UploadFileAsync(byte[] byteArray, string ext)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + ext;

            // Create a block blob  
            CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(uniqueFileName);

            // Set the object's content type  
            blockBlob.Properties.ContentType = MimeMapping.MimeUtility.GetMimeMapping(uniqueFileName);

            // upload to blob  
            using MemoryStream stream = new MemoryStream(byteArray);
            await blockBlob.UploadFromStreamAsync(stream);

            // get file uri  
            return blockBlob.Uri.AbsoluteUri;
        }

        public async Task DeleteFileAsync(string AbsoluteUri)
        {
            Uri uriObj = new Uri(AbsoluteUri);
            string BlobName = Path.GetFileName(uriObj.LocalPath);

            // get block blob refarence  
            CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(BlobName);

            // delete blob from container      
            await blockBlob.DeleteAsync();
        }

    }
}
