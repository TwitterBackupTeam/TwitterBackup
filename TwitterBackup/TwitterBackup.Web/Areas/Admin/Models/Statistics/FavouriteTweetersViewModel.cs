﻿using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class FavouriteTweetersViewModel
    {
		public IEnumerable<FavouriteTweeterDTO> FavouriteTweetersModels { get; set; }

		public long UserId { get; set; }
	}
}
