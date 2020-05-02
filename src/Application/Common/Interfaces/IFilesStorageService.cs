using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Interfaces
{
    public interface IFilesStorageService
    {
        Task<string> SaveCloudAsync(byte[] byteArray, string ext);
        Task<string> SaveLocalAsync(byte[] byteArray, string name, string ext);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="base64data"></param>
        /// <param name="ext"></param>
        /// <returns>Absolute Uri</returns>
        Task<string> SaveCloudAsync(string base64data, string ext);
        Task<string> SaveLocalAsync(string base64data, string name, string ext);
        Task DeleteCloudAsync(string absoluteUri);
        Task DeleteLocalAsync(string absolutePath);
    }
}
