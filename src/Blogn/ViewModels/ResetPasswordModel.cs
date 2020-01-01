using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogn.ViewModels
{
    public class ResetPasswordModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(384)]
        [Display(Name = "Email Address", Prompt = "Email Address")]
        public string Email { get; set; }
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
