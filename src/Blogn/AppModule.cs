using System.Reflection;
using Blogn.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn
{
    public class AppModule: IBlognModule
    {
        public void AddModuleServices(IServiceCollection services, IConfiguration configuration)
        {
        }

        public string ModuleName => typeof(AppModule).Assembly.GetName().Name;
    }
}
