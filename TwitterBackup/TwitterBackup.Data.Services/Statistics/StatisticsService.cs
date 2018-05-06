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
		private readonly IRepository<UserTweeter> userTweeterRepository;
		private readonly IRepository<UserTweet> userTweetRepository;
		
		public StatisticsService(IRepository<User> userRepository, IRepository<UserTweeter> userTweeterRepository,
								 IRepository<UserTweet> userTweetRepository,
								 IAutoMapper autoMapper, IWorkSaver workSaver) : base(autoMapper, workSaver)
		{
			this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
			this.userTweeterRepository = userTweeterRepository ?? throw new ArgumentNullException(nameof(userTweeterRepository));
			this.userTweetRepository = userTweetRepository ?? throw new ArgumentNullException(nameof(userTweetRepository));
			
		}

		public ICollection<UserStatisticsDTO> UsersStatistics()
		{
			var allUsers = this.userRepository.All().Select(user => new 
			{
				Id = user.Id,
				UserName = user.UserName,
				IsDeleted = user.IsDeleted
			}).ToList();

			var allUsersTweeters = this.userTweeterRepository.All().Select(userTweeter => new
			{
				UserName = userTweeter.User.UserName,
				IsDeleted = userTweeter.IsDeleted
			}).ToList();

			var allUserTweets = this.userTweetRepository.All().Select(userTweet => new
			{
				UserName = userTweet.User.UserName,
				IsDeleted = userTweet.IsDeleted
			}).ToList();

			Dictionary<string, UserStatisticsDTO> userStatisticsDTOs = new Dictionary<string, UserStatisticsDTO>();
			OverallStatisticsDTO overallStatisticsDTO = new OverallStatisticsDTO();

			foreach(var user in allUsers)
			{
				userStatisticsDTOs[user.UserName] = new UserStatisticsDTO
				{
					UserName = user.UserName,
					Id = user.Id,
					IsDeleted = user.IsDeleted
				};
			}

			foreach (var dto in userStatisticsDTOs)
			{ 
				dto.Value.StoredTweetsCount = allUserTweets.Count(ut => ut.UserName == dto.Key && !ut.IsDeleted);
				dto.Value.DeletedTweetsCount = allUserTweets.Count(ut => ut.UserName == dto.Key && ut.IsDeleted);
				dto.Value.FavouriteTweetersCount = allUsersTweeters.Count(ut => ut.UserName == dto.Key && !ut.IsDeleted);
				dto.Value.DeletedFavouriteTweetersCount = allUsersTweeters.Count(ut => ut.UserName == dto.Key && ut.IsDeleted);

				overallStatisticsDTO.TotalFavouriteTweetersCount += dto.Value.FavouriteTweetersCount;
				overallStatisticsDTO.TotalStoredTweetsCount += dto.Value.StoredTweetsCount;
			}

			StatisticsDTO statisticsDTO = new StatisticsDTO() { };
			
		}
	}
}