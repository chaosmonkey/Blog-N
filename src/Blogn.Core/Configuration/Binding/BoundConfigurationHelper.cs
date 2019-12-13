using System.Collections.Generic;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Binding
{
	public static class BoundConfigurationHelper
	{
		public static IBoundConfiguration AddBoundConfigurations(
			this IServiceCollection services, 
			IConfiguration configuration, 
			IEnumerable<Assembly> assemblies = null)
		{
			var builder = new BoundConfigurationBuilder(services, configuration);
			var provider = builder.Build(assemblies);
			return new BoundConfiguration(services, provider);
		}
	}
}
