using System.Threading.Tasks;

namespace ITFriends.Web.Models.Services
{
    public interface IEmailSenderRepository
    {
        /// <summary>
        /// Send an email
        /// </summary>
        /// <param name="email">Email of the reciver</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email body</param>
        /// <returns></returns>
        Task SendEmailAsync(string email, string subject, string message);
    }
}
