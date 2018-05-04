using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrators")]
	public class StatisticsController : Controller
	{
		private readonly IFavouriteTweetersStatisticsService tweetersStatisticsService;
		private readonly IStoredTweetsStatisticsService tweetsStatisticsService;

		public StatisticsController(IFavouriteTweetersStatisticsService tweetersStatisticsService,
									IStoredTweetsStatisticsService tweetsStatisticsService)
		{
			this.tweetersStatisticsService = tweetersStatisticsService;
			this.tweetsStatisticsService = tweetsStatisticsService;
		}

		public IActionResult Index()
		{
			/////////
			return this.View();/////
		}

		public IActionResult FavouriteTweeters(long userId)
		{
			var favouriteTweeters = this.tweetersStatisticsService.GetFavouriteTweetersByUserId(userId);

			//////
			return this.View();//////////
		}

		public IActionResult DeletedFavouriteTweeters(long userId)
		{
			var deletedFavouriteTweeters = this.tweetersStatisticsService.GetDeletedFavouriteTweetersByUserId(userId);


			//////////

			return this.View();//////
		}

		public IActionResult StoredTweets(long userId)
		{
			var storedTweets = this.tweetsStatisticsService.GetStoredTweetsByUserId(userId);

			//////////
			return this.View();/////////
		}

		public IActionResult DeletedTweets(long userId)
		{
			var deletedTweets = this.tweetsStatisticsService.GetDeletedTweetsByUserId(userId);

			///////
			return this.View();////////
		}
	}
}