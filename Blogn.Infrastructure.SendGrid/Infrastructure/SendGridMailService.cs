using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Blogn.Infrastructure.SendGrid.Configuration;
using ChaosMonkey.Guards;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Blogn.Infrastructure.SendGrid.Infrastructure
{
    public class SendGridMailService:IMailService
    {
        public SendGridMailService(ILogger<SendGridMailService> logger, SendGridSettings settings)
        {
            Logger = Guard.IsNotNull(logger, nameof(logger));
            Settings = Guard.IsNotNull(settings, nameof(settings));
        }

        protected ILogger Logger { get; }
        protected  SendGridSettings Settings { get; }

        public async Task<HttpStatusCode> SendAsync(string toEmail, string toName, string fromAddress, string fromName, 
                            string subject, string content, string htmlContent = null)
        {
            var message = new SendGridMessage();
            message.SetFrom(fromAddress, fromName);
            message.AddTo(toEmail, toName);
            message.SetSubject(subject);
            message.AddContent(MimeType.Text, content);
            if (htmlContent != null)
            {
                message.AddContent(MimeType.Html, htmlContent);
            }

            var client = new SendGridClient(Settings.ApiKey);

            var response = await client.SendEmailAsync(message);

            return response.StatusCode;
        }
    }
}
