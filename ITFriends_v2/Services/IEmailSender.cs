using System.Threading.Tasks;

namespace ITFriends_v2.Models.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);

        string Url { get; set; }
    }
}
