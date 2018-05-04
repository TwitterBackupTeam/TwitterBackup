using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class DeletedTweetsViewModel
    {
		public IEnumerable<DeletedTweetDTO> DeletedTweetsModels { get; set; }

		public long UserId { get; set; }
	}
}
