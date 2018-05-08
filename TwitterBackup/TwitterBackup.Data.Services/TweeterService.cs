using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.DTO.UserManagementDTOs;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
	public class TweeterService : DatabaseService, ITweeterService
	{
		private readonly ITwitterAPIService twitterAPIService;

		public TweeterService(ITwitterAPIService twitterAPIService, IAutoMapper autoMapper, IUnitOfWork unitOfWork) : base(autoMapper, unitOfWork)
		{
			this.twitterAPIService = twitterAPIService ?? throw new ArgumentNullException(nameof(twitterAPIService));
		}


		public List<ListTweetersDTO> GetFavouriteTweetersByUserId(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException("User's Id cannot be null or empty!");
			}


			var favouriteTweeters = this.UnitOfWork.UsersTweeterRepository.All().Where(x => x.UserId == userId).Select(ft => new ListTweetersDTO
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
			var tweeter = this.UnitOfWork.TweeterRepository.All().FirstOrDefault(x => x.Id == tweeterId);

			return this.AutoMapper.MapTo<TweeterDTO>(tweeter);
		}

		
		public Tweeter CreateTweeter(TweeterDTO tweeter)
		{
			if (tweeter == null)
			{
				throw new ArgumentNullException("Tweeter cannot be null!");
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

		public async Task UpdateTweeterById(long tweeterId)
		{
			var tweeter = this.UnitOfWork.TweeterRepository.All().FirstOrDefault(x => x.Id == tweeterId);

			if (tweeter == null)
			{
				throw new ArgumentNullException("Tweeter does not exist!");
			}

			var updatedTweeter = await this.twitterAPIService.GetTweeterInfoById(tweeterId);

			tweeter.Name = updatedTweeter.Name;
			tweeter.ScreenName = updatedTweeter.ScreenName;
			tweeter.Location = updatedTweeter.Location;
			tweeter.ProfileImageUrl = updatedTweeter.ProfileImageUrl;
			tweeter.FollowersCount = updatedTweeter.FollowersCount;
			tweeter.StatusesCount = updatedTweeter.StatusesCount;

			this.UnitOfWork.TweeterRepository.Update(tweeter);
			await this.UnitOfWork.SaveChangesAsync();
		}

		public void DeleteTweeterById(long tweeterId)
		{

			var tweeter = this.UnitOfWork.TweeterRepository.All().FirstOrDefault(x => x.Id == tweeterId);

			if (tweeter == null)
			{
				throw new ArgumentNullException("Tweeter does not exist!");
			}

			this.UnitOfWork.TweeterRepository.Delete(tweeter);
			this.UnitOfWork.SaveChanges();
		}

		public bool DbContainsTweeter(long tweeterId)
		{
			return this.UnitOfWork.TweeterRepository.All().Any(x => x.Id == tweeterId);
		}

	}
}
