using System.Collections.Generic;
using System.Linq;

namespace Blogn.Models
{
	public static class DefaultRoles
	{
		public static Role[] Roles = new[] {Role.User};
		public static ICollection<AccountRole> DefaultAccountRoles => Roles.Select(role => new AccountRole {Role = role}).ToList();
	}
}
