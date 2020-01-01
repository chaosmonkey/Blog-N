using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class ValidateResetToken: IRequest<ValidateResetTokenResponse>
    {
        public string Token { get; set; }
    }
}
