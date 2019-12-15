using Blogn.Models;

namespace Blogn.Responses
{
    public class MyAccountResponse:ResponseBase<Account>
    {
        public static MyAccountResponse Error(string message)
        {
            return new MyAccountResponse 
            {
                Type = ResponseType.Error, 
                ErrorMessage = message
            };
        }

        public static MyAccountResponse NotFound(string message)
        {
            return new MyAccountResponse
            {
                Type = ResponseType.NotFound,
                ErrorMessage = message
            };
        }

        public static MyAccountResponse Success(Account account)
        {
            return new MyAccountResponse
            {
                Type = ResponseType.Success,
                Data=account
            };
        }
    }
}
