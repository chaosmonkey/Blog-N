using Blogn.Models;

namespace Blogn.Responses
{
    public class AuthenticationResult: ResponseBase<Account>
    {
        public static AuthenticationResult Failed(bool isLocked = false)
        {
            return new AuthenticationResult
            {
                Type = ResponseType.Error,
                ErrorMessage = (isLocked)
                    ? "The specified account is locked.  Please contact the administrator for assistance."
                    : "Authentication failed.  Please check your email and password and try again."
            };
        }

        public static AuthenticationResult Success(Account account)
        {
            return new AuthenticationResult
            {
                Type = ResponseType.Success,
                Data = account
            };
        }

        public static AuthenticationResult ServerError(string message)
        {
            return new AuthenticationResult
            {
                Type = ResponseType.Error,
                ErrorMessage = message
            };
        }

        public bool IsAuthenticated => Type == ResponseType.Success && Data != null;
    }
}
