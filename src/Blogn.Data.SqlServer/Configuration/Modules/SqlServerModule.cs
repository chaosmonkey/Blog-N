using Blogn.Constants;
using Blogn.Data;
using Blogn.Data.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Modules
{
    public class SqlServerModule: BlognModule
    {
        public override void AddModuleServices(IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddDbContext<BlogContext>(options =>
                {
                    options.UseSqlServer(configuration.GetValue<string>(SqlConstants.ConnectionStringName), builder =>
                    {
                        builder.MigrationsAssembly(SqlConstants.MigrationsAssemblyName);
                    });
                })
                .AddTransient<IAccountRepository, SqlAccountRepository>()
                .AddTransient<IDatabaseMigrator, SqlDatabaseMigrator>();
        }

    }
}
