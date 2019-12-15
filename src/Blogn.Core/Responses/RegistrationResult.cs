using Blogn.Models;

namespace Blogn.Responses
{
    public class RegistrationResult: ResponseBase<Account>
    {
        public bool IsRegistered => Type == ResponseType.Success && Data != null;

        public static RegistrationResult Success(Account account)
        {
            return new RegistrationResult
            {
                Data = account,
                Type = ResponseType.Success
            };
        }

        public static RegistrationResult AccountExists(string email)
        {
            return new RegistrationResult
            {
                Type = ResponseType.Error, 
                ErrorMessage = $"The email address '{email}' is already in use and can not be registered."
            };
        }

        public static RegistrationResult ServerError(string message)
        {
            return new RegistrationResult
            {
                Type = ResponseType.Error,
                ErrorMessage = message
            };
        }
    }
}
