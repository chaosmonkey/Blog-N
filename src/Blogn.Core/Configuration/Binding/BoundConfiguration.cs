using ChaosMonkey.Guards;
using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Binding
{
	internal class BoundConfiguration : IBoundConfiguration
	{
		internal BoundConfiguration(IServiceCollection services, IBoundConfigurationProvider provider)
		{
			Services = Guard.IsNotNull(services, nameof(services));
			Provider = Guard.IsNotNull(provider, nameof(provider));
		}

		public IServiceCollection Services { get; }
		public IBoundConfigurationProvider Provider { get; }

		public IServiceCollection CaptureProvider(ref IBoundConfigurationProvider provider)
		{
			provider = Provider;
			return Services;
		}
	}
}
