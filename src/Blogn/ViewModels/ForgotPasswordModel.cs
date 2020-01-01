using System.ComponentModel.DataAnnotations;

namespace Blogn.ViewModels
{
    public class ForgotPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(384)]
        public string Email { get; set; }
    }
}
