using System;
using System.Security.Cryptography;
using System.Text;

namespace Blogn.Services
{
	public class GravatarService : IAvatarService
	{
		public string CalculateAvatarId(string email)
		{
			var sourceBytes = Encoding.UTF8.GetBytes(email.Trim());
			using(var algorithm = MD5.Create())
            {
                var hash = algorithm.ComputeHash(sourceBytes);
			    return BitConverter.ToString(hash).ToLower().Replace("-", "");
            }
		}

		public string CalculateAvatarUrl(
			string id, 
			int size = 80,
			DefaultAvatarType type = DefaultAvatarType.Profile,
			AvatarRating rating = AvatarRating.G)
		{
			return $"https://www.gravatar.com/avatar/{id}?s={size}&r={rating}&d={TranslateAvatarType(type)}";
		}

		public string TranslateAvatarType(DefaultAvatarType type)
		{
			switch (type)
			{
				case DefaultAvatarType.Profile:
					return "mp";
				case DefaultAvatarType.Blank:
					return "blank";
				case DefaultAvatarType.Generated:
					return "retro";
				case DefaultAvatarType.NotFound:
					return "404";
				default:
					return "mp";
			}
		}
	}
}
