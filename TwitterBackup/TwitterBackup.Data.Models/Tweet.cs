using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TwitterBackup.Data.Models
{
    public class Tweet
    {
        [Key]
        public long Id;

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int RetweetCount { get; set; }

        public int FavouriteCount { get; set; }

        public Tweeter Author { get; set; }

        /// <summary>
        /// Users who contain this tweet in their favourite collection.
        /// </summary>
        public ICollection<User> UsersFavouriteTweet { get; set; }
    }
}
