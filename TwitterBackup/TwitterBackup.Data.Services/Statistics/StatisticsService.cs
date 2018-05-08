using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO.StatisticsDTOs;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;
using TwitterBackup.Data.Services.Utils;

namespace ReTwitter.Services.Data.Statistics
{
	public class StatisticsService : IStatisticsService
	{
		private readonly IUnitOfWork unitOfWork;

		public StatisticsService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public StatisticsDTO UsersStatistics()
		{
			var allUsers = this.unitOfWork.UsersRepository.All().Select(user => new 
			{
				Id = user.Id,
				UserName = user.UserName,
				IsDeleted = user.IsDeleted
			}).ToList();

			var allUsersTweeters = this.unitOfWork.UsersTweeterRepository.All().Select(userTweeter => new
			{
				UserName = userTweeter.User.UserName,
				IsDeleted = userTweeter.IsDeleted
			}).ToList();

			var allUserTweets = this.unitOfWork.UsersTweetRepository.All().Select(userTweet => new
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
					Status = (user.IsDeleted == true) ? "Deleted" : "Active"
				};
			}

			foreach (var dto in userStatisticsDTOs)
			{ 
				dto.Value.StoredTweetsCount = allUserTweets.Count(ut => ut.UserName == dto.Key && !ut.IsDeleted);
				dto.Value.DeletedTweetsCount = allUserTweets.Count(ut => ut.UserName == dto.Key && ut.IsDeleted);
				dto.Value.FavouriteTweetersCount = allUsersTweeters.Count(ut => ut.UserName == dto.Key && !ut.IsDeleted);
				dto.Value.DeletedFavouriteTweetersCount = allUsersTweeters.Count(ut => ut.UserName == dto.Key && ut.IsDeleted);

				overallStatisticsDTO.TotalFavouriteTweetersCount += dto.Value.FavouriteTweetersCount;
				overallStatisticsDTO.TotalFavouriteTweetersCount += dto.Value.DeletedFavouriteTweetersCount;
				overallStatisticsDTO.TotalStoredTweetsCount += dto.Value.StoredTweetsCount;
				overallStatisticsDTO.TotalStoredTweetsCount += dto.Value.DeletedTweetsCount;
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