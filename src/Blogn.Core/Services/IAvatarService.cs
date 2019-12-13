namespace Blogn.Services
{
	public interface IAvatarService
	{
		string CalculateAvatarId(string email);

		string CalculateAvatarUrl(
			string id, 
			int size = 80,
			DefaultAvatarType type = DefaultAvatarType.Profile,
			AvatarRating rating = AvatarRating.G);

		string TranslateAvatarType(DefaultAvatarType type);
	}
}
