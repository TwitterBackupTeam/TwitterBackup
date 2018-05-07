using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using TwitterBackup.Data.Models;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Data.Services.Utils;
using TwitterBackup.Web.Models.AddFavouriteTweetsViewModels;

namespace TwitterBackup.Web.Controllers
{
    public class AddFavouriteTweetsController : Controller
    {
        private readonly ITwitterAPIService twitterApiService;
        private readonly ITweetService tweetService;
        private readonly IUserTweetService userTweetService;
        private readonly UserManager<User> userManager;
        private readonly IAutoMapper autoMapper;

        public AddFavouriteTweetsController(ITwitterAPIService twitterApiService, ITweetService tweetService, IUserTweetService userTweetService, UserManager<User> userManager, IAutoMapper autoMapper)
        {
            this.twitterApiService = twitterApiService;
            this.tweetService = tweetService;
            this.userTweetService = userTweetService;
            this.userManager = userManager;
            this.autoMapper = autoMapper;
        }

        

        [HttpGet]
        public async Task<IActionResult> AddFavouriteTweets(string screenName)
        {
            var tweets = await this.twitterApiService.GetTweets(screenName);
            var addFavTweetVm = new AddFavouriteTweetsViewModel();
            if (tweets.Count > 0)
            {
                var userId = this.userManager.GetUserId(this.User);
                addFavTweetVm.TweeterViewModel = this.autoMapper.MapTo<AddTweeterViewModel>(tweets.First().Author);
                foreach (var tweetDto in tweets)
                {
                    var tweetVm = this.autoMapper.MapTo<AddTweetViewModel>(tweetDto);
                    tweetVm.Favourite =
                        await this.userTweetService.CheckIfTweetExistsInUserFavouriteCollection(tweetDto.Id, userId);
                    addFavTweetVm.Tweets.Add(tweetVm);
                    await this.tweetService.Add(tweetDto);
                }
            }

            return View(addFavTweetVm);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteTweets(AddFavouriteTweetsViewModel vm)
        {
            var userId = this.userManager.GetUserId(this.User);
            foreach (var addTweetViewModel in vm.Tweets)
            {
                if (await this.userTweetService.CheckIfTweetExistsInUserFavouriteCollection(addTweetViewModel.Id,
                        userId) && !addTweetViewModel.Favourite)
                {
                    await this.userTweetService.DeleteTweetFromUserFavouriteCollection(addTweetViewModel.Id,
                        userId);
                }

                if (!(await this.userTweetService.CheckIfTweetExistsInUserFavouriteCollection(addTweetViewModel.Id,
                        userId)) && addTweetViewModel.Favourite)
                {
                    await this.userTweetService.AddTweetToUserFavouriteCollection(userId, addTweetViewModel.Id);
                }
            }

            return this.RedirectToAction("FavouriteTweeters", "FavouriteTweeters");
        }
    }
}
