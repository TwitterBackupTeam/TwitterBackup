using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TwitterBackup.Data.Models.Abstract;

namespace TwitterBackup.Data.Models
{
    public class Tweeter : IDeletable
    {
		public Tweeter()
		{
			this.UserTweeters = new HashSet<UserTweeter>();
			this.Tweets = new HashSet<Tweet>();
		}

        [Key]
        public long Id { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int StatusesCount { get; set; }

		public bool IsDeleted { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? DeletedOn { get; set; }

		public ICollection<UserTweeter> UserTweeters { get; set; }

		public ICollection<Tweet> Tweets { get; set; }
	}
}
