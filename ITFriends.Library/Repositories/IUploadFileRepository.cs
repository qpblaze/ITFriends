using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ITFriends.Web.Services
{
    public interface IUploadFileRepository
    {
        /// <summary>
        /// Uploads an image to Cloudinary servers
        /// </summary>
        /// <param name="file">The image that is going to be uploaded</param>
        /// <returns>The link to the image</returns>
        Task<string> Upload(IFormFile file);
    }
}