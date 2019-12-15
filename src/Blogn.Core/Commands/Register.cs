using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class Register: IRequest<RegistrationResult>
    {
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
