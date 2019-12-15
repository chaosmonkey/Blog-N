using System.Security.Principal;
using Blogn.Responses;
using MediatR;

namespace Blogn.Commands
{
    public class EditMyAccount:IRequest<EditMyAccountResponse>
    {
        public int AccountId { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
    }
}
