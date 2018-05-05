using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TwitterBackup.Web.Models.FavouriteTweetersViewModels;

namespace TwitterBackup.Web.Controllers
{
    [Authorize]
    public class FavouriteTweetersController : Controller
    {
        // GET: /<controller>/
        public IActionResult FavouriteTweeters()
        {
            var tweets = new List<FavouriteTweeterViewModel>();
            tweets.Add(new FavouriteTweeterViewModel(){ Name = "Tweet1"});
            tweets.Add(new FavouriteTweeterViewModel() { Name = "Tweet2" });
            tweets.Add(new FavouriteTweeterViewModel() { Name = "Tweet3" });

            return View(tweets);
        }
    }
}
