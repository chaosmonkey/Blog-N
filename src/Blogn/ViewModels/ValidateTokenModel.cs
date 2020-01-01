namespace Blogn.ViewModels
{
    public class ValidateTokenModel
    {
        public bool IsValid { get; set; }
        public string ErrorMessage { get; set; }
        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);
    }
}
