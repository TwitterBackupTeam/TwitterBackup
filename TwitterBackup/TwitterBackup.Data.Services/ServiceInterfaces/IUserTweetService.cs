using System.Collections.Generic;
using System.Threading.Tasks;
using TwitterBackup.Data.DTO;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
    public interface IUserTweetService
    {
        Task<bool> AddTweetToUserFavouriteCollection(string userId, TweetDTO tweetDto);

        Task<ICollection<TweetDTO>> GetAllFavouriteTweetsFromUserId(string id);

        Task<bool> CheckIfTweetExistsInUserFavouriteCollection(long tweetId, string userId);
    }
}
