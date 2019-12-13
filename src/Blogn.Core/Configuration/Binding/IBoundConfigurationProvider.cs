namespace Blogn.Configuration.Binding
{
	public interface IBoundConfigurationProvider
	{
		T Get<T>() where T : class, new();
	}
}
