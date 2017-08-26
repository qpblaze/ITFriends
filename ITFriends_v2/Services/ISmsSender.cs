using System.Threading.Tasks;

namespace ITFriends_v2.Models.Services
{
    public interface ISmsSender
    {
        Task SendSmsAsync(string number, string message);
    }
}