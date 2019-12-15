using System.Collections.Generic;
using System.Reflection;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration
{
    public static class MappingConfiguration
    {
        public static IServiceCollection AddMappingProfiles(this IServiceCollection services, IEnumerable<Assembly> assemblies)
        {
            var mappings = new MapperConfiguration(config =>
            {
                foreach (var assembly in assemblies)
                {
                    config.AddMaps(assembly);
                }
            });

            var mapper = mappings.CreateMapper();
            return services.AddSingleton(mapper);
        }
    }
}
