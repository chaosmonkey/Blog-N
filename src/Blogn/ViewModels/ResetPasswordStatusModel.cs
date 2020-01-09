using System;

namespace Blogn.ViewModels
{
    public class ResetPasswordStatusModel
    {
        public enum StatusKey
        {
            ServerError,
            NotFound,
            ValidationError,
            Success
        }

        public ResetPasswordStatusModel(string id)
        {
            StatusKey status;
            Enum.TryParse<StatusKey>(id,out status);
            Status = status;
        }

        public StatusKey Status { get; private set; }
    }
}
