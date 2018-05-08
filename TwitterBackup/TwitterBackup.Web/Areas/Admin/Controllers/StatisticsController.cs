using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TwitterBackup.Data.Services.ServiceInterfaces.StatisticsServices;
using TwitterBackup.Web.Areas.Admin.Models.Statistics;

namespace TwitterBackup.Web.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize(Roles = "Administrator")]
	public class StatisticsController : Controller
	{
		private readonly IFavouriteTweetersStatisticsService tweetersStatisticsService;
		private readonly IStoredTweetsStatisticsService tweetsStatisticsService;
		private readonly IStatisticsService statisticsService;

		public StatisticsController(IFavouriteTweetersStatisticsService tweetersStatisticsService,
									IStoredTweetsStatisticsService tweetsStatisticsService,
									IStatisticsService statisticsService)
		{
			this.tweetersStatisticsService = tweetersStatisticsService;
			this.tweetsStatisticsService = tweetsStatisticsService;
			this.statisticsService = statisticsService;
		}

		public IActionResult Index()
		{
			var userStatisticsDTOs = this.statisticsService.UsersStatistics();

			var viewModel = new StatisticsViewModel()
			{
				UserStatisticsDTOs = userStatisticsDTOs.UserStatisticsDTOs,
				OverallStatistics = userStatisticsDTOs.OverallStatisticsDTO
			};

			return this.View(viewModel);
		}

		public IActionResult FavouriteTweeters(string userId)
		{
			var favouriteTweeters = this.tweetersStatisticsService.GetFavouriteTweetersByUserId(userId);

			var viewModel = new FavouriteTweetersViewModel
			{
				FavouriteTweetersModels = favouriteTweeters,
				UserId = userId
			};

			return this.View(viewModel);
		}

		public IActionResult DeletedFavouriteTweeters(string userId)
		{
			var deletedFavouriteTweeters = this.tweetersStatisticsService.GetDeletedFavouriteTweetersByUserId(userId);

			var viewModel = new DeletedFavouriteTweetersViewModel
			{
				DeletedFavouriteTweetersModels = deletedFavouriteTweeters,
				UserId = userId
			};

			return this.View(viewModel);
		}

		public IActionResult StoredTweets(string userId)
		{
			var storedTweets = this.tweetsStatisticsService.GetStoredTweetsByUserId(userId);

			var viewModel = new StoredTweetsViewModel
			{
				StoredTweetsModels = storedTweets,
				UserId = userId
			};

			return this.View(viewModel);
		}

		public IActionResult DeletedTweets(string userId)
		{
			var deletedTweets = this.tweetsStatisticsService.GetDeletedTweetsByUserId(userId);

			var viewModel = new DeletedTweetsViewModel
			{
				DeletedTweetsModels = deletedTweets,
				UserId = userId
			};

			return this.View(viewModel);
		}
	}
}