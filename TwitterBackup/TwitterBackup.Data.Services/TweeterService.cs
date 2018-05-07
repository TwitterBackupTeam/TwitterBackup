using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.DTO.UserManagementDTOs;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
	public class TweeterService : DatabaseService
	{
		private readonly ITwitterAPIClient twitterAPIClient;

		public TweeterService(ITwitterAPIClient twitterAPIClient, IAutoMapper autoMapper, IUnitOfWork unitOfWork) : base(autoMapper, unitOfWork)
		{
			this.twitterAPIClient = twitterAPIClient ?? throw new ArgumentNullException(nameof(twitterAPIClient));
		}


		public List<ListTweetersDTO> GetFavouriteTweetersByUserId(string userId)
		{
			if (userId == null || userId == "")
			{
				throw new ArgumentNullException("User's Id cannot be null or empty!");
			}

			var favouriteTweeters = this.UnitOfWork.UsersTweeterRepository.All().Where(w => w.UserId == userId).Select(ft => new ListTweetersDTO
			{
				Id = ft.Tweeter.Id,
				Name = ft.Tweeter.Name,
				ScreenName = ft.Tweeter.ScreenName,
				FollowersCount = ft.Tweeter.FollowersCount
			}).ToList();

			return favouriteTweeters;
		}

		public TweeterDTO GetTweeterById(long tweeterId)
		{
			var tweeter = this.UnitOfWork.TweeterRepository.All().FirstOrDefault(t => t.Id == tweeterId);

			return this.AutoMapper.MapTo<TweeterDTO>(tweeter);
		}

		
	}
}
