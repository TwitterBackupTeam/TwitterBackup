using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO.StatisticsDTOs;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;

namespace ReTwitter.Services.Data.Statistics
{
	public class StatisticsService : IStatisticsService
	{
		private readonly IWorkSaver workSaver;

		public StatisticsService(IWorkSaver workSaver)
		{
			this.workSaver = workSaver ?? throw new ArgumentNullException(nameof(workSaver));
		}

		//public ICollection<UserStatisticsDTO> UsersStatistics()
		//{
			//var allUsers = this.workSaver.UserRepository.All().Select(user => new UserStatisticsDTO
			//{
			//	Id = user.Id,
			//	ScreenName = user.Scre,

				
			//}).ToList();

			

			//foreach (var userModel in usesStatisticsModels)
			//{
				
			//}

			//var statisticsModels = new IEnumerable<UserStatisticsModel>(usesStatisticsModels.Values);

			//return statisticsModels;
		//}
	}
}