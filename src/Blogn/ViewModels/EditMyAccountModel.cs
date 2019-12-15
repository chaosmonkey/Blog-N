using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blogn.ViewModels
{
    public class EditMyAccountModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [StringLength(384)]
        [Display(Name="Email Address", Prompt = "Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(64)]
        [Display(Name = "Display Name", Prompt = "Display Name")]
        public string DisplayName { get; set; }
        public string AvatarId { get; set; }
    }
}
