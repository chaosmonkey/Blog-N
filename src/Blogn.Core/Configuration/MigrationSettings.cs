using Blogn.Configuration.Binding;

namespace Blogn.Configuration
{
	[BoundConfiguration("Migrations")]
	public class MigrationSettings
	{
		public bool IsAutomaticMigrationsEnabled { get; set; }
		public string DefaultAdminUserName { get; set; }
		public string DefaultAdminPassword { get; set; }
	}
}
