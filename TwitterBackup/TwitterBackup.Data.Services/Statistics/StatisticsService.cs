using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO.StatisticsDTOs;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;
using TwitterBackup.Data.Services.Utils;

namespace ReTwitter.Services.Data.Statistics
{
	public class StatisticsService : DatabaseService, IStatisticsService
	{
		private readonly IRepository<User> userRepository;

		public StatisticsService(IRepository<User> userRepository, IAutoMapper autoMapper, IWorkSaver workSaver) : base(autoMapper, workSaver)
		{
			this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
		}

		public ICollection<UserStatisticsDTO> UsersStatistics()
		{
			var allUsers = this.workSaver.UserRepository.All().Select(user => new UserStatisticsDTO
			{
				Id = user.Id,
				ScreenName = user.,


			}).ToList();



			//foreach (var userModel in usesStatisticsModels)
			//{

			//}

			//var statisticsModels = new IEnumerable<UserStatisticsModel>(usesStatisticsModels.Values);

			//return statisticsModels;
		}
	}
}