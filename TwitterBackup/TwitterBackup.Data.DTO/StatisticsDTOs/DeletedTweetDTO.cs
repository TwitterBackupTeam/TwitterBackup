using Newtonsoft.Json;
using System;

namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class DeletedTweetDTO
    {
		public long Id { get; set; }

		public string AuthorScreenName { get; set; }

		public string Text { get; set; }

		public string CreatedAtStr { get; set; }

		public DateTime DeletedOn { get; set; }
	}
}
