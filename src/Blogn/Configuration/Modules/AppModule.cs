using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Modules
{
    public class AppModule: BlognModule
    {
        public override void AddModuleServices(IServiceCollection services, IConfiguration configuration)
        {
        }
    }
}
