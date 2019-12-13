namespace Blogn.Models
{
	public class AccountRole
	{
		public int AccountId { get; set; }
		public Account Account { get; set; }
		public Role Role { get; set; }
	}
}
