using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace ITFriends_v2.Services
{
    public interface IUploadImageService
    {
        /// <summary>
        /// Uploads an image to Cloudinary servers
        /// </summary>
        /// <param name="file">The image that is going to be uploaded</param>
        /// <returns>The link to the image</returns>
        Task<string> Upload(IFormFile file);
    }
}