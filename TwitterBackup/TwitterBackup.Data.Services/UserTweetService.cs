using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TwitterBackup.Data.DTO;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public class UserTweetService : DatabaseService, IUserTweetService
    {
        private readonly IRepository<UserTweet> userTweetRepository;
        private readonly ITweetService tweetService;

        public UserTweetService(IRepository<UserTweet> userTweetRepo, ITweetService tweetService, IAutoMapper autoMapper, IWorkSaver workSaver) : base(autoMapper, workSaver)
        {
            this.userTweetRepository = userTweetRepo;
            this.tweetService = tweetService;
        }

        public async Task<bool> AddTweetToUserFavouriteCollection(string userId, TweetDTO tweetDto)
        {
            if (this.tweetService.GetTweetById(tweetDto.Id) == null)
            {
                await this.tweetService.Add(tweetDto);
            }

            this.userTweetRepository.Add(new UserTweet(){ UserId = userId, TweetId = tweetDto.Id});

            return await this.WorkSaver.SaveChangesAsync();
        }

        public async Task<ICollection<TweetDTO>> GetAllFavouriteTweetsFromUserId(string id)
        {
            var collection = new List<TweetDTO>();

            foreach (var userTweet in await this.userTweetRepository.All().Where(u => u.UserId == id).ToListAsync())
            {
                collection.Add(this.tweetService.GetTweetById(userTweet.TweetId));
            }

            return collection;
        }
    }
}
