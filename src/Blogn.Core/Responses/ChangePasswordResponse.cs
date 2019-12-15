namespace Blogn.Responses
{
    public class ChangePasswordResponse:ResponseBase
    {
        public static ChangePasswordResponse Error(string message)
        {
            return new ChangePasswordResponse
            {
                Type = ResponseType.Error, 
                ErrorMessage = message
            };
        }

        public static ChangePasswordResponse ServerError(string message)
        {
            return new ChangePasswordResponse
            {
                Type = ResponseType.Error,
                ErrorMessage = message
            };
        }

        public static ChangePasswordResponse Success()
        {
            return new ChangePasswordResponse
            {
                Type = ResponseType.Success
            };
        }
        
        public static ChangePasswordResponse NotFound(string message)
        {
            return new ChangePasswordResponse
            {
                Type = ResponseType.NotFound,
                ErrorMessage = message
            };
        }
    }
}
