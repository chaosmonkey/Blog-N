using System;
using System.Collections.Generic;
using System.Text;

namespace Blogn.Responses
{
    public class ValidateResetTokenResponse: ResponseBase
    {
        public static ValidateResetTokenResponse NotFound()
        {
            return new ValidateResetTokenResponse
            {
                Type = ResponseType.NotFound
            };
        }

        public static ValidateResetTokenResponse Error(string message)
        {
            return new ValidateResetTokenResponse
            {
                Type = ResponseType.Error, 
                ErrorMessage = message
            };
        }

        public static ValidateResetTokenResponse Success()
        {
            return new ValidateResetTokenResponse
            {
                Type = ResponseType.Success
            };
        }
    }
}
