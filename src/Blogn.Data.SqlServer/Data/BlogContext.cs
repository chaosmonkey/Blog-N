using Blogn.Data.EntityTypeConfigurations;
using Blogn.Models;
using Microsoft.EntityFrameworkCore;

namespace Blogn.Data
{
	public class BlogContext: DbContext
	{
		public BlogContext()
		{
		}

		public BlogContext(DbContextOptions<BlogContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new AccountEntityConfiguration())
                .ApplyConfiguration(new CredentialsEntityConfiguration())
                .ApplyConfiguration(new AccountRoleEntityConfiguration())
                .ApplyConfiguration(new ResetTokenEntityConfiguration());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Account> Accounts { get; set; }
		public DbSet<AccountRole> AccountRoles { get; set; }
		public DbSet<Credentials> Credentials { get; set; }
        public DbSet<ResetToken> ResetTokens { get; set; }
	}
}
