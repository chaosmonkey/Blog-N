using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class ChangePassword:IRequest<ChangePasswordResponse>
    {
        public int AccountId { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
