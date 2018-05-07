using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TwitterBackup.Data.DTO.StatisticsDTOs;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;

namespace TwitterBackup.Data.Services
{
	public class StoredTweetsStatisticsService : IStoredTweetsStatisticsService
	{
		private readonly IRepository<UserTweet> userTweetRepository;

		public StoredTweetsStatisticsService(IRepository<UserTweet> userTweetRepository)
		{
			this.userTweetRepository = userTweetRepository ?? throw new ArgumentNullException(nameof(userTweetRepository));
		}

		public ICollection<StoredTweetDTO> GetStoredTweetsByUserId(string userId)
		{
			if (string.IsNullOrWhiteSpace(userId))
			{
				throw new ArgumentException("UserId cannot be null");
			}

			var savedTweets = this.userTweetRepository.All().Where(u => u.UserId == userId).Select(s =>
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

			var deletedTweets = this.userTweetRepository.All().Where(u => u.UserId == userId && u.IsDeleted).Select(s =>
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
