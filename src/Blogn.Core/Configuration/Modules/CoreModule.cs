using Blogn.Models;
using Blogn.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Modules
{
    public class CoreModule: BlognModule
    {
        public override void AddModuleServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IPasswordHasher<Account>, PasswordHasher<Account>>()
                .AddSingleton<IPasswordHasher, PasswordHasher>()
                .AddSingleton<IAvatarService, GravatarService>()
                .AddSingleton<ITimeProvider, DefaultTimeProvider>();
        }

    }
}
