using System.Threading.Tasks;
using Blogn.Models;

namespace Blogn.Services
{
	public interface IAuthenticationManager
	{
		Task SignInAsync(Account account);
		Task SignOutAsync();
	}
}
