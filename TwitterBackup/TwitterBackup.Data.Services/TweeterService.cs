using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.DTO.UserManagementDTOs;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
	public class TweeterService : DatabaseService
	{
		private readonly ITwitterAPIService twitterAPIService;

		public TweeterService(ITwitterAPIService twitterAPIService, IAutoMapper autoMapper, IUnitOfWork unitOfWork) : base(autoMapper, unitOfWork)
		{
			this.twitterAPIService = twitterAPIService ?? throw new ArgumentNullException(nameof(twitterAPIService));
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

		public bool DbContainsTweeter(long tweeterId)
		{
			return this.UnitOfWork.TweeterRepository.All().Any(t => t.Id == tweeterId);
		}

		public Tweeter CreateTweeter(TweeterDTO tweeter)
		{
			if (tweeter == null)
			{
				throw new ArgumentNullException("Followee cannot be null!");
			}

			Tweeter newTweeter = new Tweeter
			{
				Id = tweeter.Id,
				Name = tweeter.Name,
				ScreenName = tweeter.ScreenName,
				Location = tweeter.Location,
				Description = tweeter.Description,
				ProfileImageUrl = tweeter.ProfileImageUrl,
				FollowersCount = tweeter.FollowersCount,
				StatusesCount = tweeter.StatusesCount
			};

			this.UnitOfWork.TweeterRepository.Add(newTweeter);
			this.UnitOfWork.SaveChanges();
			return newTweeter;
		}

		public void UpdateTweeterById(long tweeterId)
		{
			
		}


	}
}
