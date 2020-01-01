namespace Blogn.Responses
{
    public class ForgotPasswordResponse: ResponseBase
    {
        public static ForgotPasswordResponse NotFound()
        {
            return new ForgotPasswordResponse
            {
                Type = ResponseType.NotFound
            };
        }

        public static ForgotPasswordResponse Error(string message)
        {
            return new ForgotPasswordResponse
            {
                Type = ResponseType.Error,
                ErrorMessage = message
            };
        }

        public static ForgotPasswordResponse Success()
        { 
            return new ForgotPasswordResponse
            {
                Type = ResponseType.Processing
            };
        }

        public bool ShowSuccess => (Type != ResponseType.Error);
    }
}
