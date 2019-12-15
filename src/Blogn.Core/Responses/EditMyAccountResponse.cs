namespace Blogn.Responses
{
    public class EditMyAccountResponse: ResponseBase
    {
        public static EditMyAccountResponse Success()
        {
            return new EditMyAccountResponse
            {
                Type = ResponseType.Success
            };
        }

        public static EditMyAccountResponse NotFound(string message)
        {
            return new EditMyAccountResponse
            {
                Type = ResponseType.NotFound,
                ErrorMessage = message
            };
        }

        public static EditMyAccountResponse EmailExists(string message)
        {
            return new EditMyAccountResponse
            {
                Type = ResponseType.Error,
                ErrorMessage = message
            };
        }

        public static EditMyAccountResponse ServerError(string message)
        {
            return new EditMyAccountResponse
            {
                Type = ResponseType.Error,
                ErrorMessage = message
            };
        }

    }
}
