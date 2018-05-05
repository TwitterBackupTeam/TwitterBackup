namespace TwitterBackup.Data.Models
{
    public class UserTweet
    {
        public string UserId { get; set; }
        public User User { get; set; }

        public long TweetId { get; set; }
        public Tweet Tweet { get; set; }
    }
}
