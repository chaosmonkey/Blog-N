using System.Collections.Generic;

namespace Blogn.ViewModels
{
    public class MyAccountModel
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public List<string> RoleNames { get; set; }
        public string AvatarId { get; set; }
    }
}
