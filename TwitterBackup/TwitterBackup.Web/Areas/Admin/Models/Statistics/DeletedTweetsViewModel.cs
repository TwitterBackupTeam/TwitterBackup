using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class DeletedTweetsViewModel
    {
		public ICollection<DeletedTweetDTO> DeletedTweetsModels { get; set; }

		public string UserId { get; set; }
	}
}
