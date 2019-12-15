using System.Linq;
using System.Net;
using ChaosMonkey.Guards;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Blogn.TagHelpers
{
	[HtmlTargetElement("a", Attributes = "with-return-url")]
	public class WithReturnUrlTagHelper: TagHelper
	{

		private const string TargetAttribute = "with-return-url";

		public WithReturnUrlTagHelper(IHttpContextAccessor accessor)
		{
			HttpContext = Guard.IsNotNull(accessor, nameof(accessor)).HttpContext;
		}

		protected HttpContext HttpContext { get; }

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var currentUrl = HttpContext.Request.GetEncodedPathAndQuery();
			if (output.Attributes.TryGetAttribute("href", out TagHelperAttribute href))
			{
				if (!string.IsNullOrWhiteSpace(href.Value.ToString()))
				{
					var url = href.Value.ToString();
					var target = (url.Contains("?"))
						? $"{url}&ReturnUrl={WebUtility.UrlEncode(currentUrl)}"
						: $"{url}?ReturnUrl={WebUtility.UrlEncode(currentUrl)}";
					var targetAttribute = output.Attributes.SingleOrDefault(attr => string.Equals(attr.Name, "href"));
					if (targetAttribute != null)
					{
						output.Attributes.SetAttribute("href", target);
					}
				}
			}
			base.Process(context, output);
		}
	}
}
