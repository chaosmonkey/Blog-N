using System;
using System.Collections.Generic;
using ChaosMonkey.Guards;

namespace Blogn.Configuration.Binding
{
	internal class BoundConfigurationProvider : IBoundConfigurationProvider
	{
		internal BoundConfigurationProvider(Dictionary<Type, object> configurations)
		{
			Configurations = Guard.IsNotNull(configurations, nameof(configurations));
		}

		protected Dictionary<Type, object> Configurations { get; }

		public T Get<T>() where T : class, new()
		{
			if (Configurations.ContainsKey(typeof(T)))
			{
				return Configurations[typeof(T)] as T;
			}

			return default(T);
		}
	}
}
