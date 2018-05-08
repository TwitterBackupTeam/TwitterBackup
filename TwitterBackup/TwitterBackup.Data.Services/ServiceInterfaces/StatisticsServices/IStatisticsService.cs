using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices
{
	public interface IStatisticsService
    {
		StatisticsDTO UsersStatistics();
	}
}
