using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Blogn.ViewModels
{
    public class RegistrationModel
    {
        [Required]
        [StringLength(64)]
        [DisplayName("Display Name")]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(384)]
        [DataType(DataType.EmailAddress)]
        [DisplayName("Email Address")]
        public string Email { get; set; }

        [Required]
        [StringLength(384)]
        [DataType(DataType.EmailAddress)]
        [Compare("Email")]
        [DisplayName("Confirm Email Address")]
        public string ConfirmEmail { get; set; }

        [Required]
        [StringLength(256)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [StringLength(256)]
        [DataType(DataType.Password)]
        [Compare("Password")]
        [DisplayName("Confirm Password")]
        public string ConfirmPassword { get; set; }
    }
}
