using System.Collections.Generic;

namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class StatisticsDTO
    {
		public ICollection<UserStatisticsDTO> UserStatisticsDTOs;

		public OverallStatisticsDTO OverallStatisticsDTO { get; set; }
	}
}
