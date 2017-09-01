namespace ITFriends.Library
{
    /// <summary>
    /// Cloudinary api secrets
    /// http://cloudinary.com
    /// </summary>
    public class CloudinarySecrets
    {
        public string CloudName { get; set; }
        public string ApiKey { get; set; }
        public string ApiSecret { get; set; }
    }

    /// <summary>
    /// SendGrid api secrets
    /// https://sendgrid.com/
    /// </summary>
    public class SendGridSecrets
    {
        public string ApiKey { get; set; }
    }

    public class AppSecrets
    {
        public CloudinarySecrets Cloudinary { get; set; }
        public SendGridSecrets SendGrid { get; set; }
    }
}