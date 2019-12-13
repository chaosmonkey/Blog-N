using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Modules
{
    public abstract class BlognModule:IBlognModule
    {
        public abstract void AddModuleServices(IServiceCollection services, IConfiguration configuration);
        
        public string ModuleName => typeof(CoreModule).Assembly.GetName().Name;
    }
}
