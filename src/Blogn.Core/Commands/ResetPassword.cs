using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class ResetPassword:IRequest<ResetPasswordResponse>
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string Token { get; set; }
    }
}
