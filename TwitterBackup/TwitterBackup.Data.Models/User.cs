using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace TwitterBackup.Data.Models
{
    public class User : IdentityUser
    {
        public User()
        {
            this.UserTweets = new HashSet<UserTweet>();
        }

        public ICollection<UserTweet> UserTweets { get; set; }

		public string TwitterName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }
	}
}
