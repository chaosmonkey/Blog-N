using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class Authenticate: IRequest<AuthenticationResult>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
