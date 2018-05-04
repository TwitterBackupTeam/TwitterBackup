using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices
{
	public interface IStoredTweetsStatisticsService
    {
		IEnumerable<StoredTweetDTO> GetStoredTweetsByUserId(long userId);
		IEnumerable<DeletedTweetDTO> GetDeletedTweetsByUserId(long userId);
	}
}
