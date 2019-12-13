using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ChaosMonkey.Guards;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Binding
{
	public class BoundConfigurationBuilder
	{
		public BoundConfigurationBuilder(IServiceCollection services, IConfiguration configuration)
		{
			Services = Guard.IsNotNull(services, nameof(services));
			Configuration = Guard.IsNotNull(configuration, nameof(configuration));
		}

		protected  IConfiguration Configuration { get; }
		protected IServiceCollection Services { get; }

		public IBoundConfigurationProvider Build(IEnumerable<Assembly> assemblies)
		{
			if (assemblies == null)
			{
				assemblies = new []{Assembly.GetExecutingAssembly()};
			}
			var boundTypes = assemblies.SelectMany(assembly =>
				assembly.GetTypes()
					.Where(type => (type.IsPublic && (type.GetCustomAttribute(typeof(BoundConfigurationAttribute)) != null)))
			).ToList();

			var registrations = new Dictionary<Type, object>();
			foreach (var type in boundTypes)
			{
				var attribute = type.GetCustomAttribute(typeof(BoundConfigurationAttribute)) as BoundConfigurationAttribute;
				var settings = Activator.CreateInstance(type);
				var section = attribute?.SectionName ?? type.Name;
				Configuration.Bind(section, settings);
				Services.AddSingleton(type, settings);
				registrations.Add(type, settings);
			}

			return new BoundConfigurationProvider(registrations);
		}

		public IBoundConfigurationProvider Build()
		{
			return Build(null);
		}
	}
}
