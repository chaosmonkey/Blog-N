using System.Security.Claims;
using Blogn.Services;
using ChaosMonkey.Guards;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Blogn.TagHelpers
{
	[HtmlTargetElement("avatar", TagStructure = TagStructure.WithoutEndTag)]
	public class AvatarTagHelper:TagHelper
	{
		public AvatarTagHelper(IAvatarService service)
		{
			Service = Guard.IsNotNull(service, nameof(service));
		}

		protected IAvatarService Service { get; }

		public string AvatarId { get; set; }

		public ClaimsPrincipal ForPrincipal { get; set; }

		[HtmlAttributeName("alt")]
		public string AlternateText { get; set; }

		public string Title { get; set; }

		public int Size { get; set; }

		public DefaultAvatarType AvatarType { get; set; } = DefaultAvatarType.Profile;

		public AvatarRating AvatarRating { get; set; } = AvatarRating.G;

		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			var avatarId = (ForPrincipal != null) ? ForPrincipal.GetAvatarId() : AvatarId;
			var altText = (ForPrincipal != null) ? ForPrincipal.GetNameOrAnonymous() : AlternateText;
			var title = (ForPrincipal != null) ? altText : Title ?? altText;
			output.TagName = "img";
			output.Attributes.Add("src", Service.CalculateAvatarUrl(avatarId, Size, AvatarType, AvatarRating));
			output.Attributes.Add("alt", altText);
			output.Attributes.Add("title", title);
			base.Process(context, output);
		}
	}
}
