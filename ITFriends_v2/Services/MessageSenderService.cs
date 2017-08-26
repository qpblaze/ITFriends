using ITFriends_v2.Models.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ITFriends_v2
{
    public class MessageSenderService : IEmailSender, ISmsSender
    {
        /// <summary>
        /// Sendgrid credintials
        /// https://sendgrid.com/
        /// </summary>
        private readonly string API_KEY;

        /// <summary>
        /// Domain URL
        /// </summary>
        public string Url { get; set; }

        public MessageSenderService(IOptions<AppSecrets> options)
        {
            // Initialize API secrets
            API_KEY = options.Value.SendGrid.ApiKey;
        }

        /// <summary>
        /// Custom credintials (can be anything)
        /// </summary>
        private const string EMAIL = "itfriends@ursaciuc-adrian.tech";
        private const string NAME = "IT Friends";

        /// <summary>
        /// Send an email using SendGrid api
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