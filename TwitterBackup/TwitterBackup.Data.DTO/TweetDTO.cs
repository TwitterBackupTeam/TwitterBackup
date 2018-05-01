using System;

namespace TwitterBackup.Data.DTO
{
    public class TweetDTO
    {
        public long Id { get; set; }

        public string Text { get; set; }

        public DateTime CreatedAt { get; set; }

        public int RetweetCount { get; set; }

        public int FavouriteCount { get; set; }

        public TweeterDTO Author { get; set; }

        public string HashTags { get; set; }
    }
}
