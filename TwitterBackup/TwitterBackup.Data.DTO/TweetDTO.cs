using Newtonsoft.Json;

namespace TwitterBackup.Data.DTO
{
	public class TweetDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAtStr { get; set; }

        [JsonProperty("retweet_count")]
        public int RetweetCount { get; set; }

        [JsonProperty("favorite_count")]
        public int FavouriteCount { get; set; }

        [JsonProperty("user")]
        public TweeterDTO Author { get; set; }
    }
}
