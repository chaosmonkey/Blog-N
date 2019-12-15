using System;
using System.Linq;
using System.Security.Claims;
using Blogn.Constants;

namespace Blogn
{
	public static class ClaimsPrincipalExtensions
	{
		public static int GetAccountId(this ClaimsPrincipal principal)
		{
			return Convert.ToInt32(principal?.GetClaim(ClaimTypes.NameIdentifier) ?? "0");
		}

		public static string GetName(this ClaimsPrincipal principal)
		{
			return principal?.GetClaim(ClaimTypes.Name) ?? string.Empty;
		}

		public static string GetNameOrAnonymous(this ClaimsPrincipal principal)
		{
			return principal?.GetClaim(ClaimTypes.Name) ?? "Anonymous";
		}

		public static string GetAvatarId(this ClaimsPrincipal principal)
		{
			return principal?.GetClaim(CustomClaimTypes.AvatarId) ?? string.Empty;
		}

		private static string GetClaim(this ClaimsPrincipal principal, string type)
		{
			return principal?.Claims?.SingleOrDefault(claim => claim.Type == type)?.Value;
		}
	}
}
