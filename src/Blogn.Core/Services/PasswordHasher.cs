using Blogn.Models;
using ChaosMonkey.Guards;
using Microsoft.AspNetCore.Identity;

namespace Blogn.Services
{
	public class PasswordHasher : IPasswordHasher
	{
		public PasswordHasher(IPasswordHasher<Account> hasher)
		{
			Hasher = Guard.IsNotNull(hasher, nameof(hasher));
		}

		protected IPasswordHasher<Account> Hasher { get; }

		public string HashPassword(string password)
		{
			return Hasher.HashPassword(null, password);
		}

		public PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
		{
			return Hasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
		}
	}
}
