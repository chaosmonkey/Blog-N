using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class GetMyAccount: IRequest<MyAccountResponse>
    {
        public int AccountId { get; set; }
    }
}
