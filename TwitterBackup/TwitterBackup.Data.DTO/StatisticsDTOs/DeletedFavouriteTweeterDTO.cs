using Newtonsoft.Json;
using System;

namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class DeletedFavouriteTweeterDTO
    {
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("screen_name")]
		public string ScreenName { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
