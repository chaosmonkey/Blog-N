using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class ForgotPassword: IRequest<ForgotPasswordResponse>
    {
        public string Email { get; set; }
        public string SiteProtocol { get; set; } = "https";
        public string SiteDomain { get; set; }
    }
}
