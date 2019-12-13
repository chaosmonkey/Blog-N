using Microsoft.Extensions.DependencyInjection;

namespace Blogn.Configuration.Binding
{
	public interface IBoundConfiguration
	{
		IServiceCollection Services { get; }
		IBoundConfigurationProvider Provider { get; }
		IServiceCollection CaptureProvider(ref IBoundConfigurationProvider provider);
	}
}
