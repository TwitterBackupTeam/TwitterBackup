using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO.StatisticsDTOs;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;
using TwitterBackup.Data.Services.Utils;

namespace ReTwitter.Services.Data.Statistics
{
	public class StatisticsService : DatabaseService, IStatisticsService
	{		
		public StatisticsService(IAutoMapper autoMapper, IUnitOfWork unitOfWork) : base(autoMapper, unitOfWork)
		{ }

		public StatisticsDTO UsersStatistics()
		{
			var allUsers = this.UnitOfWork.UsersRepository.All().Select(user => new 
			{
				Id = user.Id,
				UserName = user.UserName,
				IsDeleted = user.IsDeleted
			}).ToList();

			var allUsersTweeters = this.UnitOfWork.UsersTweeterRepository.All().Select(userTweeter => new
			{
				UserName = userTweeter.User.UserName,
				IsDeleted = userTweeter.IsDeleted
			}).ToList();

			var allUserTweets = this.UnitOfWork.UsersTweetRepository.All().Select(userTweet => new
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

			StatisticsDTO statisticsDTO = new StatisticsDTO()
			{
				UserStatisticsDTOs = userStatisticsDTOs.Values,
				OverallStatisticsDTO = overallStatisticsDTO
			};

			return statisticsDTO;
		}
	}
}