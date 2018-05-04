using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class DeletedFavouriteTweetersViewModel
    {
		public IEnumerable<DeletedFavouriteTweeterDTO> DeletedFavouriteTweetersModels { get; set; }

		public long UserId { get; set; }
	}
}
