using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface ICloudStorageService
    {
        Task<string> UploadFileAsync(byte[] byteArray, string ext);
        Task DeleteFileAsync(string AbsoluteUri);
    }
}
