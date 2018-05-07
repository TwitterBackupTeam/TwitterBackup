using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;
using TwitterBackup.Web.Models.AddFavouriteTweetsViewModels;
using TwitterBackup.Web.Models.FavouriteTweetersViewModels;

namespace TwitterBackup.Web.Controllers
{
    public class AddFavouriteTweetsController : Controller
    {
        private readonly ITwitterAPIService twitterApiService;
        private readonly IUserTweetService userTweetService;
        private readonly UserManager<User> userManager;
        private readonly IAutoMapper autoMapper;

        public AddFavouriteTweetsController(ITwitterAPIService twitterApiService, IUserTweetService userTweetService, UserManager<User> userManager, IAutoMapper autoMapper)
        {
            this.twitterApiService = twitterApiService;
            this.userTweetService = userTweetService;
            this.userManager = userManager;
            this.autoMapper = autoMapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteTweets(string screenName)
        {
            var tweets = await this.twitterApiService.GetTweets(screenName);
            var addFavTweetVm = new AddFavouriteTweetsViewModel();
            var userId = this.userManager.GetUserId(this.User);
            addFavTweetVm.TweeterViewModel = this.autoMapper.MapTo<TweeterViewModel>(tweets.First().Author);
            foreach (var tweetDto in tweets)
            {
                var tweetVm = this.autoMapper.MapTo<AddTweetViewModel>(tweetDto);
                tweetVm.Favourite = await this.userTweetService.CheckIfTweetExistsInUserFavouriteCollection(tweetDto.Id, userId);
                addFavTweetVm.Tweets.Add(tweetVm);
            }

            return View(addFavTweetVm);
        }

        [HttpPost]
        public IActionResult Save(AddFavouriteTweetsViewModel model)
        {

            return this.RedirectToAction("FavouriteTweeters", "FavouriteTweeters");
        }
    }
}
