using System;
using ChaosMonkey.Guards;

namespace Blogn.Configuration.Binding
{
	public class BoundConfigurationAttribute: Attribute
	{
		public BoundConfigurationAttribute(string sectionName)
		{
			SectionName = Guard.IsNotNullOrWhitespace(sectionName, nameof(SectionName));
		}

		public string SectionName { get; }
	}
}
