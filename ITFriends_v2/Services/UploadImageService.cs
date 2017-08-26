using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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
        private readonly string CLOUD_NAME;
        private readonly string API_KEY;
        private readonly string API_SECRET;

        public UploadImageService(IOptions<AppSecrets> options)
        {
            // Initialize API secrets
            CLOUD_NAME = options.Value.Cloudinary.CloudName;
            API_KEY = options.Value.Cloudinary.ApiKey;
            API_SECRET = options.Value.Cloudinary.ApiSecret;
        }

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