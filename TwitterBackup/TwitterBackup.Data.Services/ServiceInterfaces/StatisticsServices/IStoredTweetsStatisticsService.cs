using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices
{
	public interface IStoredTweetsStatisticsService
    {
		ICollection<StoredTweetDTO> GetStoredTweetsByUserId(string userId);

		ICollection<DeletedTweetDTO> GetDeletedTweetsByUserId(string userId);
	}
}
