using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices
{
	public interface IFavouriteTweetersStatisticsService
    {
		IEnumerable<FavouriteTweeterDTO> GetFavouriteTweetersByUserId(long userId);

		IEnumerable<DeletedFavouriteTweeterDTO> GetDeletedFavouriteTweetersByUserId(long userId);
	}
}
