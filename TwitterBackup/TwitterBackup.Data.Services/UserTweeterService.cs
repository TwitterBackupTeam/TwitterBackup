using System;
using System.Linq;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Data.Services
{
	public class UserTweeterService : IUserTweeterService
	{
		private readonly IUnitOfWork unitOfWork;
		private readonly ITweeterService tweeterService;

		public UserTweeterService(IUnitOfWork unitOfWork, ITweeterService tweeterService)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
		}

		public bool DbContainsUserTweeter(string userId, long tweeterId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			return this.unitOfWork.UsersTweeterRepository.All()
				.Any(x => x.TweeterId == tweeterId && x.UserId == userId);
		}

		public virtual bool UserTweeterIsDeleted(string userId, long tweeterId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			return this.unitOfWork.UsersTweeterRepository
				.All().Any(x => x.TweeterId == tweeterId && x.UserId == userId && x.IsDeleted);
		}

		public void SaveUserTweeter(string userId, TweeterDTO tweeter)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			var assignTweeterToUser = this.unitOfWork.TweeterRepository.All().FirstOrDefault(x => x.Id == tweeter.Id);

			if (assignTweeterToUser == null)
			{
				assignTweeterToUser = this.tweeterService.CreateTweeter(tweeter);
				var userTweeter = new UserTweeter
				{
					UserId = userId,
					TweeterId = assignTweeterToUser.Id
				};

				this.unitOfWork.UsersTweeterRepository.Add(userTweeter);
				this.unitOfWork.SaveChanges();
			}
			else
			{
				if (assignTweeterToUser.IsDeleted)
				{
					assignTweeterToUser.IsDeleted = false;
					assignTweeterToUser.DeletedOn = null;

					this.unitOfWork.SaveChanges();
				}

				if (!this.DbContainsUserTweeter(userId, tweeter.Id))
				{
					var userTweeter = new UserTweeter
					{
						UserId = userId,
						TweeterId = assignTweeterToUser.Id
					};

					this.unitOfWork.UsersTweeterRepository.Add(userTweeter);
					this.unitOfWork.SaveChanges();
				}
				else
				{
					var restoreUserTweeter =
						this.unitOfWork.UsersTweeterRepository.All().FirstOrDefault(x =>
							x.TweeterId == tweeter.Id && x.UserId == userId && x.IsDeleted);

					if (restoreUserTweeter != null)
					{
						restoreUserTweeter.IsDeleted = false;
						restoreUserTweeter.DeletedOn = null;

						this.unitOfWork.SaveChanges();
					}
				}
			}
		}

		public void DeleteUserTweeter(string userId, long tweeterId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			var userTweeter =
				this.unitOfWork.UsersTweeterRepository.All().FirstOrDefault(x => x.TweeterId == tweeterId && x.UserId == userId && !x.IsDeleted);

			if (userTweeter != null)
			{
				this.unitOfWork.UsersTweeterRepository.Delete(userTweeter);
				this.unitOfWork.SaveChanges();
			}
		}
	}
}
