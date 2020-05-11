using System.Threading.Tasks;

namespace SkipperAgency.Application.Common.Interfaces
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
        /// <summary>
        /// Uploads a new file to the cloud and deletes a file connected to an oldAbsoluteUri if it exists.
        /// </summary>
        /// <param name="base64data"></param>
        /// <param name="ext"></param>
        /// <param name="oldAbsoluteUri"></param>
        /// <returns>Absolute Uri of the new file.</returns>
        Task<string> ReplaceCloudAsync(string base64data, string ext, string oldAbsoluteUri);
    }
}
