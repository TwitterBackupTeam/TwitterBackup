using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Data.Services.ServiceInterfaces;
using TwitterBackup.Web.Models.FavouriteTweetersViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class FavouriteTweetersController : Controller
    {
        private readonly ITwitterService twitterService;

        public FavouriteTweetersController(ITwitterService twitterService)
        {
            this.twitterService = twitterService;
        }

        // GET: /<controller>/
        public IActionResult FavouriteTweeters()
        {
            var tweets = new List<FavouriteTweeterViewModel>();
            tweets.Add(new FavouriteTweeterViewModel(){ Name = "Tweet1"});
            tweets.Add(new FavouriteTweeterViewModel() { Name = "Tweet2" });
            tweets.Add(new FavouriteTweeterViewModel() { Name = "Tweet3" });
            foreach (var tweetDto in this.twitterService.GetTweets("trump"))
            {
                tweets.Add(new FavouriteTweeterViewModel() { Name = tweetDto.Text});
            }

            return View(tweets);
        }
    }
}
