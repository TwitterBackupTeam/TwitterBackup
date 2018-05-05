using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices
{
	public interface IFavouriteTweetersStatisticsService
    {
		ICollection<FavouriteTweeterDTO> GetFavouriteTweetersByUserId(long userId);

		ICollection<DeletedFavouriteTweeterDTO> GetDeletedFavouriteTweetersByUserId(long userId);
	}
}
