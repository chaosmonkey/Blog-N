using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChaosMonkey.Guards;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration
{
    public class BlognModuleCollection
    {
        public BlognModuleCollection(IServiceCollection services, IConfiguration configuration)
        {
            Services = Guard.IsNotNull(services, nameof(services));
            Configuration = Guard.IsNotNull(configuration, nameof(configuration));

            LoadedAssemblies = AppDomain.CurrentDomain.GetAssemblies();

            var moduleNames = Configuration.GetSection("BlognModules").GetChildren().Select(module => module.Value).ToArray();
            var moduleTypes = LoadedAssemblies
                    .SelectMany(assembly => assembly.GetTypes()
                    .Where(type => type.GetInterface("IBlognModule") !=null && !type.IsInterface && !type.IsAbstract));
            Modules = moduleTypes
                    .Select(module=> Activator.CreateInstance(module) as IBlognModule)
                    .Where(module=>module!=null && moduleNames.Contains(module.ModuleName))
                    .ToList();
        }

        protected IEnumerable<Assembly> LoadedAssemblies { get; }
        protected IServiceCollection Services { get; }
        protected IConfiguration Configuration { get; }
        protected List<IBlognModule> Modules { get; }

        public IServiceCollection AddServices()
        {
            Modules.ForEach(module=>module.AddModuleServices(Services, Configuration));
            return Services;
        }

        public void Configure()
        {
        }

    }
}
