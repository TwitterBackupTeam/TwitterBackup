using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class FavouriteTweetersViewModel
    {
		public ICollection<FavouriteTweeterDTO> FavouriteTweetersModels { get; set; }

		public string UserId { get; set; }
	}
}
