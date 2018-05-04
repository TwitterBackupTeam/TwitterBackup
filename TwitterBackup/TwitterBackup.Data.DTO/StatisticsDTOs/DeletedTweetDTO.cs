using Newtonsoft.Json;
using System;

namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class DeletedTweetDTO
    {
		[JsonProperty("id")]
		public long Id { get; set; }

		[JsonProperty("user")]
		public TweeterDTO Author { get; set; }

		[JsonProperty("text")]
		public string Text { get; set; }

		[JsonProperty("created_at")]
		public string CreatedAtStr { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
