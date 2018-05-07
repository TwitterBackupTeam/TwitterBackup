using System;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;

namespace TwitterBackup.Data.Services
{
	public class CascadeDeleteEntityService
    {
		private readonly IAdminUserService userService;
		private readonly IUnitOfWork unitOfWork;
		private readonly IUserTweetService userTweetService;
		private readonly ITweetService tweetService;

		public CascadeDeleteEntityService(IAdminUserService userService, IUnitOfWork unitOfWork,
										  IUserTweetService userTweetService, ITweetService tweetService)
		{
			this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
			this.userTweetService = userTweetService ?? throw new ArgumentNullException(nameof(userTweetService));
			this.tweetService = tweetService ?? throw new ArgumentNullException(nameof(tweetService));
			
		}


		public void CascadeDeleteUser(string userId)
		{
			
		}

		public virtual void CascadeDeleteFavouriteTweeter(string userId, string tweeterId)
		{

		}

		public virtual void CascadeDeleteTweet(string userId, string tweetId)
		{
		}
	}
}
