using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Data.Models.Abstract;

namespace TwitterBackup.Data.Models
{
	public class User : IdentityUser, IDeletable
    {
        public User()
        {
            this.UserTweets = new HashSet<UserTweet>();
        }

        public ICollection<UserTweet> UserTweets { get; set; }

		public string TwitterName { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }
	}
}
