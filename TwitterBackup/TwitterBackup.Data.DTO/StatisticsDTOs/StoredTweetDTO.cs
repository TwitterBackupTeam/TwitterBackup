using Newtonsoft.Json;
using System;

namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class StoredTweetDTO
    {
		public long Id { get; set; }

		public string AuthorScreenName { get; set; }

		public string Text { get; set; }

		public string CreatedAtStr { get; set; }
	}
}
