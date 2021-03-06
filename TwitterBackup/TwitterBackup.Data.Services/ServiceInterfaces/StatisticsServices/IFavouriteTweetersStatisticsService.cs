﻿using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices
{
	public interface IFavouriteTweetersStatisticsService
    {
		ICollection<FavouriteTweeterDTO> GetFavouriteTweetersByUserId(string userId);

		ICollection<DeletedFavouriteTweeterDTO> GetDeletedFavouriteTweetersByUserId(string userId);
	}
}
