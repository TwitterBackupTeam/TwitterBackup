using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices
{
	public interface IStoredTweetsStatisticsService
    {
		ICollection<StoredTweetDTO> GetStoredTweetsByUserId(long userId);
		ICollection<DeletedTweetDTO> GetDeletedTweetsByUserId(long userId);
	}
}
