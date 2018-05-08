using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
    public interface IUserTweetService
    {
        Task<bool> AddTweetToUserFavouriteCollection(string userId, TweetDTO tweetDto);

        Task<bool> AddTweetToUserFavouriteCollection(string userId, long tweetId);

        Task<ICollection<TweetDTO>> GetAllFavouriteTweetsFromUserId(string id);

        Task<bool> CheckIfUserHasTweetFromTweeterId(long tweeterId, string userId);

        Task<bool> CheckIfTweetExistsInUserFavouriteCollection(long tweetId, string userId);

        Task<bool> DeleteTweetFromUserFavouriteCollection(long tweetId, string userId);
    
		void DeleteUserTweet(string userId, long tweetId);

		bool DbContainsTweet(long tweetId);
	}
}
