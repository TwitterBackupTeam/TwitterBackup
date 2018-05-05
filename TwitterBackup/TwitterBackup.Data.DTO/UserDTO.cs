using Newtonsoft.Json;

namespace TwitterBackup.Data.DTO
{
	public class UserDTO
	{
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("followers_count")]
		public int FollowersCount { get; set; }

		[JsonProperty("screen_name")]
		public string ScreenName { get; set; }
	}

}
