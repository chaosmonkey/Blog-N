using Blogn.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn
{
    public class CoreModule:IBlognModule
    {
        public void AddModuleServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public string ModuleName => typeof(CoreModule).Assembly.GetName().Name;
    }
}
