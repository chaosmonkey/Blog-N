using Blogn.Configuration.Modules;
using Blogn.Infrastructure.SendGrid.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Infrastructure.SendGrid.Configuration.Modules
{
    public class SendGridModule: BlognModule
    {
        public override void AddModuleServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IMailService, SendGridMailService>();
        }
    }
}
