using System;
using System.Collections.Generic;
using System.Linq;

namespace Blogn.Models
{
	public class Account
	{
		public Account()
		{
			Roles = new HashSet<AccountRole>();
		}

		public int Id { get; set; }
		public string DisplayName { get; set; }
		public string Email { get; set; }
		public string AvatarId { get; set; }
		public bool IsEnabled { get; set; }
        public DateTimeOffset DateCreated { get; set; }
        public DateTimeOffset DateUpdated { get; set; }
        public ICollection<AccountRole> Roles { get; set; }
		public Credentials Credentials { get; set; }
		public IEnumerable<string> RoleNames => Roles?.Select(role => role.Role.ToString());
	}
}
