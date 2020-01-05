using Blogn.Configuration.Binding;

namespace Blogn.Infrastructure.SendGrid.Configuration
{
    [BoundConfiguration("SendGrid")]
    public class SendGridSettings
    {
        public string ApiKey { get; set; }
    }
}
