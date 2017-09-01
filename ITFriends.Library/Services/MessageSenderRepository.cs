using ITFriends.Library;
using ITFriends.Web.Models.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace ITFriends.Web
{
    public class MessageSenderService : IEmailSenderRepository, ISmsSender
    {
        #region Private Properties

        /// <summary>
        /// Sendgrid credintials
        /// https://sendgrid.com/
        /// </summary>
        private readonly string API_KEY;

        /// <summary>
        /// Custom credintials (can be anything)
        /// </summary>
        private const string EMAIL = "itfriends@ursaciuc-adrian.tech";
        private const string NAME = "IT Friends";

        #endregion Private Properties

        #region Default Constructor

        public MessageSenderService(IOptions<AppSecrets> options)
        {
            // Initialize API secrets
            API_KEY = options.Value.SendGrid.ApiKey;
        }

        #endregion Default Constructor

        

        /// <summary>
        /// Send an email using SendGrid API
        /// </summary>
        /// <param name="email">Email of the reciver</param>
        /// <param name="subject">Email subject</param>
        /// <param name="message">Email body</param>
        /// <returns></returns>
        public async Task SendEmailAsync(string email, string subject, string message)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress(EMAIL, NAME);
            var to = new EmailAddress(email, "");
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", message);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        public Task SendSmsAsync(string number, string message)
        {
            throw new NotImplementedException();
        }
    }
}