using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Data.Models
{
    public class Tweet
    {
        public Tweet()
        {
            this.UserTweets = new HashSet<UserTweet>();
        }

        [Key]
        public long Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int RetweetCount { get; set; }

        public int FavouriteCount { get; set; }

        public Tweeter Author { get; set; }

        /// <summary>
        /// Users who happen to have this tweet in their favourite collection.
        /// </summary>
        public ICollection<UserTweet> UserTweets { get; set; }
    }
}
