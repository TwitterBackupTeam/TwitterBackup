using System;
using System.Linq;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Data.Services
{
	public class CascadeDeleteEntityService : ICascadeDeleteEntityService
	{
		private readonly IAdminUserService userService;
		private readonly IUnitOfWork unitOfWork;
		private readonly IUserTweetService userTweetService;
		private readonly ITweetService tweetService;
		private readonly ITweeterService tweeterService;
		private readonly IUserTweeterService userTweeterService;

		public CascadeDeleteEntityService(IAdminUserService userService, IUnitOfWork unitOfWork,
										  IUserTweetService userTweetService, ITweetService tweetService)
		{
			this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			this.userTweetService = userTweetService ?? throw new ArgumentNullException(nameof(userTweetService));
			this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
			this.userTweeterService = userTweeterService ?? throw new ArgumentNullException(nameof(userTweeterService));
			this.tweeterService = tweeterService ?? throw new ArgumentNullException(nameof(tweeterService));
		}


		public void CascadeDeleteUser(string userId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			var pairedTweeters = this.unitOfWork.UsersTweeterRepository.All().Where(w => w.UserId == userId).Select(ut => ut.TweeterId).ToList();

			this.userService.DeleteUserByUserId(userId);

			if (pairedTweeters.Any())
			{
				foreach (var tweeterId in pairedTweeters)
				{
					this.CascadeDeleteUserTweeter(userId, tweeterId);
				}
			}
		}

		public virtual void CascadeDeleteUserTweeter(string userId, long tweeterId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			this.userTweeterService.DeleteUserTweeter(userId, tweeterId);

			if (!this.tweeterService.DbContainsTweeter(tweeterId))
			{
				this.tweeterService.DeleteTweeterById(tweeterId);
			}

			var tweetIds = this.unitOfWork.UsersTweetRepository.All().Where(x => x.Tweet.Id == tweeterId && x.UserId == userId && !x.IsDeleted).Select(s => s.TweetId).ToList();

			if (tweetIds.Any())
			{
				foreach (var tweetId in tweetIds)
				{
					this.CascadeDeleteTweet(userId, tweetId);
				}
			}
		}

		public virtual void CascadeDeleteTweet(string userId, long tweetId)
		{
			if (string.IsNullOrEmpty(userId))
			{
				throw new ArgumentNullException(nameof(userId));
			}

			this.userTweetService.DeleteUserTweet(userId, tweetId);

			if (!this.userTweetService.DbContainsTweet(tweetId))
			{
				this.tweetService.Delete(tweetId);				
			}
		}
	}
}
