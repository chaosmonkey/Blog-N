using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Blogn.Constants;
using Blogn.Models;
using ChaosMonkey.Guards;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;

namespace Blogn.Services
{
	public class AuthenticationManager : IAuthenticationManager
	{
		public AuthenticationManager(IHttpContextAccessor contextAccessor)
		{
			Guard.IsNotNull(contextAccessor, nameof(contextAccessor));
			Context = contextAccessor.HttpContext;
		}

		private HttpContext Context { get; }

		public async Task SignInAsync(Account account)
		{
			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.Name, account.DisplayName),
				new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
				new Claim(ClaimTypes.Email, account.Email),
				new Claim(CustomClaimTypes.AvatarId, account.AvatarId)
			};
			claims.AddRange(account.Roles.Select(role => new Claim(ClaimTypes.Role, role.Role.ToString())));
			var principal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role));
			await Context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
		}

		public async Task SignOutAsync()
		{
			await Context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
	}
}
