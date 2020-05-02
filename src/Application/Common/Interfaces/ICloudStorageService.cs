using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICloudStorageService
    {
        /// <summary>
        /// Uploads the file to the Cloud Storage with Guid file name and the given extension.
        /// </summary>
        /// <param name="byteArray">Byte Array of the file to be uploaded.</param>
        /// <param name="ext">File extension (with or without ".").</param>
        /// <returns>Absolute File Uri.</returns>
        Task<string> UploadFileAsync(byte[] byteArray, string ext);
        Task DeleteFileAsync(string AbsoluteUri);
    }
}
