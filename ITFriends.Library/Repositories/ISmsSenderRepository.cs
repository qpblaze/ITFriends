using System.Threading.Tasks;

namespace ITFriends.Web.Models.Services
{
    public interface ISmsSender
    {
        /// <summary>
        /// Send a SMS message
        /// </summary>
        /// <param name="number">The number of the reciver</param>
        /// <param name="message">The message content</param>
        /// <returns></returns>
        Task SendSmsAsync(string number, string message);
    }
}