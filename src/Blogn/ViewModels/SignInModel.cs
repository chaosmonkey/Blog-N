using System.ComponentModel.DataAnnotations;

namespace Blogn.ViewModels
{
    public class SignInModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Address", Prompt = "Email Address")]
        [StringLength(384)]
        public string Email { get; set; }

        [Required]
        [StringLength(256, MinimumLength =8)]
        [DataType(DataType.Password)]
        [Display(Name="Password", Prompt ="Password")]
        public string Password { get; set; }
    }
}
