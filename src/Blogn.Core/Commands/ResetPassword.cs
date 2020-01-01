using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class ResetPassword:IRequest<ResetPasswordResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Token { get; set; }
    }
}
