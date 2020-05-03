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
    /// <summary>
    /// This class knows how to work with a container given the accessKey.
    /// </summary>
    public abstract class AzureStorageService
    {
        private CloudBlobContainer _blobContainer;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accessKey"></param>
        /// <param name="containerName"></param>
        protected AzureStorageService(string accessKey, string containerName)
        {
            Init(accessKey, containerName);
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

        protected async Task<string> UploadFileAsync(byte[] byteArray, string ext)
        {
            string uniqueFileName = Guid.NewGuid().ToString();
            if (ext.StartsWith("."))
                uniqueFileName = string.Join("", uniqueFileName, ext);
            else
                uniqueFileName = string.Join(".", uniqueFileName, ext);

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

        protected async Task DeleteFileAsync(string absoluteUri)
        {
            Uri uriObj = new Uri(absoluteUri);
            string blobName = Path.GetFileName(uriObj.LocalPath);

            // get block blob refarence  
            CloudBlockBlob blockBlob = _blobContainer.GetBlockBlobReference(blobName);

            // delete blob from container      
            await blockBlob.DeleteAsync();
        }

    }
}
