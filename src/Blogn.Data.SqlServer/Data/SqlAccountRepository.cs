using System.Threading;
using System.Threading.Tasks;
using Blogn.Models;
using ChaosMonkey.Guards;
using Microsoft.EntityFrameworkCore;

namespace Blogn.Data
{
	public class SqlAccountRepository: IAccountRepository
	{
		public SqlAccountRepository(BlogContext context)
		{
			Db = Guard.IsNotNull(context, nameof(context));
		}

		protected BlogContext Db { get; }

		public Task<Account> RetrieveAccountAsync(string email)
		{
			return Db.Accounts.Include(account => account.Roles).Include(account => account.Credentials)
				.SingleOrDefaultAsync(account => account.Email.ToLower() == email.ToLower());
		}

		public Task<Account> RetrieveAccountAsync(int id)
		{
			return Db.Accounts.Include(account => account.Roles).SingleOrDefaultAsync(account => account.Id == id);
		}

		public Task<bool> CheckIfAccountExistsAsync(string email)
        {
            var address = email?.ToLower() ?? string.Empty;
			return Db.Accounts.AnyAsync(account => account.Email.ToLower() == email.ToLower());
		}

		public void AddAccount(Account account)
		{
			Db.Accounts.Add(account);
		}

		public void UpdateAccount(Account account)
		{
			Db.Accounts.Update(account);
		}

        public Task<Credentials> RetrieveCredentialsAsync(int accountId)
        {
            return Db.Credentials.SingleOrDefaultAsync(credentials => credentials.AccountId == accountId);
        }

        public void UpdateCredentials(Credentials credentials)
		{
			Db.Credentials.Update(credentials);
		}

		public Task SaveAsync()
		{
			return Db.SaveChangesAsync();
		}

		public Task SaveAsync(CancellationToken token)
		{
			return Db.SaveChangesAsync(token);
		}
	}
}
