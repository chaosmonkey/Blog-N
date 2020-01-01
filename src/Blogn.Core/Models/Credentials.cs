using System;
using System.Collections;
using System.Collections.Generic;

namespace Blogn.Models
{
	public class Credentials
	{
		public int AccountId { get; set; }
		public string Password { get; set; }
		public DateTimeOffset DateCreated { get; set; }
		public DateTimeOffset DateUpdated { get; set; }
		public Account Account { get; set; }
        public ICollection<ResetToken> ResetTokens { get; set; }
	}
}
