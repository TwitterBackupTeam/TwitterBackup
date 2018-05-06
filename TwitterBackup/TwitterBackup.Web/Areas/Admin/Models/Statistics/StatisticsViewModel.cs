using System.Collections.Generic;
using TwitterBackup.Data.DTO.StatisticsDTOs;

namespace TwitterBackup.Web.Areas.Admin.Models.Statistics
{
	public class StatisticsViewModel
    {
		public ICollection<UserStatisticsDTO> UserStatisticsDTOs { get; set; }
		public OverallStatisticsDTO OverallStatistics { get; set; }
	}
}
