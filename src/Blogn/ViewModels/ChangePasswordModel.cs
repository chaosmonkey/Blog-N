using System.ComponentModel.DataAnnotations;

namespace Blogn.ViewModels
{
    public class ChangePasswordModel
    {
        [Required]
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 8)]
        [Display(Name="Current Password", Prompt = "Current Password")]
        public string CurrentPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 8)]
        [Display(Name = "New Password", Prompt = "New Password")]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [StringLength(256, MinimumLength = 8)]
        [Display(Name = "Confirm Password", Prompt = "Confirm Password")]
        [Compare("NewPassword")]
        public string ConfirmPassword { get; set; }
    }
}
