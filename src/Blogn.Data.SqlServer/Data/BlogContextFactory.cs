using System.IO;
using Blogn.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Blogn.Data
{
	public class BlogContextFactory : IDesignTimeDbContextFactory<BlogContext>
	{
		public BlogContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddEnvironmentVariables()
                .AddJsonFile("appsettings.json")
                .AddUserSecrets("56d3bc0e-4caa-4ef5-8682-be770f7597fd")
                .Build();
			var connectionString = config.GetValue<string>(SqlConstants.ConnectionStringName);
            
			var builder = new DbContextOptionsBuilder<BlogContext>();
			builder.UseSqlServer(connectionString);
			return new BlogContext(builder.Options);
		}
	}
}
