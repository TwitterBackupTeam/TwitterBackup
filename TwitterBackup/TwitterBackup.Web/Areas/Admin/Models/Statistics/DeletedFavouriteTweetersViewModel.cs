using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class DeletedFavouriteTweetersViewModel
    {
		public ICollection<DeletedFavouriteTweeterDTO> DeletedFavouriteTweetersModels { get; set; }

		public string UserId { get; set; }
	}
}
