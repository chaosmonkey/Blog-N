using System.Threading;
using System.Threading.Tasks;
using Blogn.Models;

namespace Blogn.Data
{
	public interface IAccountRepository
	{
		Task<Account> RetrieveAccountAsync(string email);
		Task<Account> RetrieveAccountAsync(int id);
		Task<bool> CheckIfAccountExistsAsync(string email);
		void AddAccount(Account account);
		void UpdateAccount(Account account);

        Task<Credentials> RetrieveCredentialsAsync(int accountId);
		void UpdateCredentials(Credentials credentials);

		Task SaveAsync();
		Task SaveAsync(CancellationToken token);
	}
}
