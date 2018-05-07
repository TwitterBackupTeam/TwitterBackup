using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO.StatisticsDTOs;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;

namespace ReTwitter.Services.Data.Statistics
{
	public class FavouriteTweetersStatisticsService : IFavouriteTweetersStatisticsService
	{
		private readonly IUnitOfWork unitOfWork;

		public FavouriteTweetersStatisticsService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public ICollection<FavouriteTweeterDTO> GetFavouriteTweetersByUserId(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException("UserId cannot be null");
			}

			var favouriteTweeters = this.unitOfWork.UsersTweeterRepository.All().Where(u => u.UserId == userId).Select(s =>
				new FavouriteTweeterDTO
				{
					Id = s.TweeterId,
					ScreenName = s.Tweeter.ScreenName,
					Description = s.Tweeter.Description
				}).ToList();

			return favouriteTweeters;
		}

		public ICollection<DeletedFavouriteTweeterDTO> GetDeletedFavouriteTweetersByUserId(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException("UserId cannot be null");
			}
			var deletedeFollowees = this.unitOfWork.UsersTweeterRepository.All().Where(u => u.UserId == userId && u.IsDeleted).Select(s =>
				new DeletedFavouriteTweeterDTO
				{
					ScreenName = s.Tweeter.ScreenName,
					Description = s.Tweeter.Description,
					DeletedOn = s.DeletedOn.Value
				}).ToList();

			return deletedeFollowees;
		}
	}
}