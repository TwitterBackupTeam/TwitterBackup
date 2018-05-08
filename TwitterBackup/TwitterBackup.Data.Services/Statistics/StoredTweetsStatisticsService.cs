using System;
using System.Collections.Generic;
using System.Linq;
using TwitterBackup.Data.DTO.StatisticsDTOs;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;

namespace ReTwitter.Services.Data.Statistics
{
	public class StoredTweetsStatisticsService : IStoredTweetsStatisticsService
	{
		private readonly IUnitOfWork unitOfWork;

		public StoredTweetsStatisticsService(IUnitOfWork unitOfWork)
		{
			this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
		}

		public ICollection<StoredTweetDTO> GetStoredTweetsByUserId(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException("UserId cannot be null");
			}

			var savedTweets = this.unitOfWork.UsersTweetRepository.All().Where(u => u.UserId == userId).Select(s =>
				new StoredTweetDTO
				{
					Id = s.TweetId,
					AuthorScreenName = s.Tweet.Author.ScreenName,
					Text = s.Tweet.Text
				}).ToList();

			return savedTweets;
		}

		public ICollection<DeletedTweetDTO> GetDeletedTweetsByUserId(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException("UserId cannot be null");
			}

			var deletedTweets = this.unitOfWork.UsersTweetRepository.All().Where(u => u.UserId == userId && u.IsDeleted).Select(s =>
				new DeletedTweetDTO
				{
					AuthorScreenName = s.Tweet.Author.ScreenName,
					Text = s.Tweet.Text,
					DeletedOn = s.DeletedOn.Value
				}).ToList();

			return deletedTweets;
		}
	}
}
