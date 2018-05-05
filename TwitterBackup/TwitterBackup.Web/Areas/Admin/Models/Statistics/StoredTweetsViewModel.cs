using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class StoredTweetsViewModel
    {
		public ICollection<StoredTweetDTO> StoredTweetsModels { get; set; }

		public long UserId { get; set; }
	}
}
