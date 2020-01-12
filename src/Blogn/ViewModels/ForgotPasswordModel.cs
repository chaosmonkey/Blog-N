using System.ComponentModel.DataAnnotations;

namespace Blogn.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(384)]
        [Display(Name = "Email Address", Prompt = "Email Address")]
        public string Email { get; set; }
    }
}
