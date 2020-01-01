using Blogn.Configuration.Binding;

namespace Blogn.Configuration
{
    [BoundConfiguration("Mail")]
    public class MailSettings
    {
        public string SystemFromAddress { get; set; }
        public string SystemDisplayName { get; set; }
    }
}
