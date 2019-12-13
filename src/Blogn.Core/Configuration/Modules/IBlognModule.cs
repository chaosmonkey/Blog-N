using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration
{
    public interface IBlognModule
    {
        void AddModuleServices(IServiceCollection services, IConfiguration configuration);
        
        string ModuleName { get; }
    }
}
