using Newtonsoft.Json;

namespace TwitterBackup.Data.DTO
{
    public class TweeterDTO
    {
        [JsonProperty("id")]
        public long Id { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("location")]
        public string Location { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("profile_background_image_url")]
        public string ProfileImageUrl { get; set; }

        [JsonProperty("followers_count")]
        public int FollowersCount { get; set; }

        [JsonProperty("statuses_count")]
        public int StatusesCount { get; set; }
    }
}
