using System;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Data.Models.Abstract;

namespace TwitterBackup.Data.Models
{
    public class UserTweet : IDeletable
    {
        public int UserId { get; set; }
        public User User { get; set; }

        public int TweetId { get; set; }
        public Tweet Tweet { get; set; }

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }
	}
}
