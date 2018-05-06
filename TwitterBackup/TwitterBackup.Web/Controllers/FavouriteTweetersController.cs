using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;
using TwitterBackup.Web.Models.FavouriteTweetersViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class FavouriteTweetersController : Controller
    {
        private readonly IUserTweetService userTweetService;
        private readonly UserManager<User> userManager;
        private readonly IAutoMapper autoMapper;

        public FavouriteTweetersController(IUserTweetService userTweetService, UserManager<User> userManager, IAutoMapper autoMapper)
        {
            this.userTweetService = userTweetService;
            this.userManager = userManager;
            this.autoMapper = autoMapper;
        }

        // GET: /<controller>/
        public async Task<IActionResult> FavouriteTweeters()
        {
            var tweets = new List<FavouriteTweeterViewModel>();
            var userId = this.userManager.GetUserId(this.User);
            var collection = await this.userTweetService.GetAllFavouriteTweetsFromUserId(userId);
            var tweeters = collection.GroupBy(t => t.Author.Id);
            foreach (var tweeter in tweeters)
            {
                FavouriteTweeterViewModel favTweeterVM = null;
                foreach (var tweetDto in tweeter)
                {
                    if (favTweeterVM is null)
                    {
                        favTweeterVM = this.autoMapper.MapTo<FavouriteTweeterViewModel>(tweetDto.Author);
                    }

                    favTweeterVM.Tweets.Add(this.autoMapper.MapTo<TweetViewModel>(tweetDto));
                }

                tweets.Add(favTweeterVM);
            }


            return View(tweets);
        }
    }
}
