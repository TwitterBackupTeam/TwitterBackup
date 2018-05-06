using Newtonsoft.Json;

namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class FavouriteTweeterDTO
    {
		[JsonProperty("id")]
		public string Id { get; set; }

		[JsonProperty("screen_name")]
		public string ScreenName { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
