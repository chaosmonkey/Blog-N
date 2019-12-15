using ChaosMonkey.Guards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Blogn.TagHelpers
{
	[HtmlTargetElement("*", Attributes = ForAttributeName)]
	public class ForAuthenticatedTagHelper : TagHelper
	{
		private const string ForAttributeName = "for-authenticated";

		public ForAuthenticatedTagHelper(IHttpContextAccessor accessor)
		{
			Context = Guard.IsNotNull(accessor, nameof(accessor)).HttpContext;
		}

		protected HttpContext Context { get; }

		[HtmlAttributeName(ForAttributeName)]
		public bool ForAuthenticated { get; set; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			if (ForAuthenticated != (Context?.User?.Identity?.IsAuthenticated ?? false))
			{
				output.SuppressOutput();
			}
			base.Process(context, output);
		}
	}
}
