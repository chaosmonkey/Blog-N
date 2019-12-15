using System.Collections.Generic;
using System.Linq;
using Blogn.Configuration;
using Blogn.Constants;
using Blogn.Models;
using Blogn.Services;
using ChaosMonkey.Guards;
using Microsoft.EntityFrameworkCore;

namespace Blogn.Data.Migrations
{
	public class SqlDatabaseMigrator: IDatabaseMigrator
	{
		public SqlDatabaseMigrator(BlogContext db, MigrationSettings settings, IPasswordHasher hasher, IAvatarService avatarService, ITimeProvider time)
		{
			Db = Guard.IsNotNull(db, nameof(db));
			Settings = Guard.IsNotNull(settings, nameof(settings));
			Hasher = Guard.IsNotNull(hasher, nameof(hasher));
			AvatarService = Guard.IsNotNull(avatarService, nameof(avatarService));
            Time = Guard.IsNotNull(time, nameof(time));
        }

		protected BlogContext Db { get; }
		protected MigrationSettings Settings { get; }
		protected IPasswordHasher Hasher { get; }
		protected IAvatarService AvatarService { get; }
        protected ITimeProvider Time { get; }

		public void Migrate()
		{
			if (Settings.IsAutomaticMigrationsEnabled)
			{
				Db.Database.Migrate();
				if (!Db.Accounts.Any())
                {
                    var now = Time.NowUtc;
					var email = Settings.DefaultAdminUserName ?? Defaults.Administrator.UserName;
					var hash = Hasher.HashPassword(Settings.DefaultAdminPassword ?? Defaults.Administrator.Password);
					var avatarId = AvatarService.CalculateAvatarId(email);
					var admin = new Account
					{
                        DateCreated = now,
                        DateUpdated = now,
						Email = email,
						Roles = new List<AccountRole> {new AccountRole {Role = Role.Administrator}},
						Credentials = new Credentials
						{
							DateCreated = now,
							DateUpdated = now,
							Password = hash 
						},
						AvatarId = avatarId,
						DisplayName = Defaults.Administrator.DisplayName,
						IsEnabled = true
					};
					Db.Accounts.Add(admin);
					Db.SaveChanges();
				}
			}
		}
	}
}
