using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace ITFriends_v2.Services
{
    public class UploadImageService : IUploadImageService
    {
        /// <summary>
        /// Cloudinary credintial
        /// http://cloudinary.com
        /// </summary>
        private const string CLOUD_NAME = "itfriendscloud-v1";
        private const string API_KEY = "473916817971436";
        private const string API_SECRET = "S_S0CSKbWFMVh9-Se8ZXKxWLQLg";

        /// <summary>
        /// Uploads an image to Cloudinary servers
        /// </summary>
        /// <param name="file">The image that is going to be uploaded</param>
        /// <returns>The link to the image</returns>
        public async Task<string> Upload(IFormFile file)
        {
            // Create an account with the credintials
            var account = new CloudinaryDotNet.Account(CLOUD_NAME, API_KEY, API_SECRET);

            // Create the needed objects to upload the image
            var cloudinary = new Cloudinary(account);
            var uploadParams = new ImageUploadParams();

            // Get the file content using a StreamReader
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                uploadParams.File = new FileDescription(file.FileName, reader.BaseStream);
            }

            // Upload the image to the server
            var uploadResult = await cloudinary.UploadAsync(uploadParams).ConfigureAwait(false);

            // Return image link
            return uploadResult.Uri.OriginalString;
        }
    }
}