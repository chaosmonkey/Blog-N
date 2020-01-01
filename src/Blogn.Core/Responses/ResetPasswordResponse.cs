namespace Blogn.Responses
{
    public class ResetPasswordResponse: ResponseBase
    {
        public static ResetPasswordResponse NotFound()
        {
            return new ResetPasswordResponse
            {
                Type = ResponseType.NotFound
            };
        }

        public static ResetPasswordResponse Error(string message)
        {
            return new ResetPasswordResponse
            {
                Type = ResponseType.Error,
                ErrorMessage = message
            };
        }

        public static ResetPasswordResponse Success()
        {
            return new ResetPasswordResponse
            {
                Type = ResponseType.Success
            };
        }
    }
}
