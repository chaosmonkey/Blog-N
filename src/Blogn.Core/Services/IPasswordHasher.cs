using Microsoft.AspNetCore.Identity;

namespace Blogn.Services
{
	public interface IPasswordHasher
	{
		string HashPassword(string password);
		PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword);
	}
}