using System.Collections.Generic;

namespace TwitterBackup.Web.Models.FavouriteTweetersViewModels
{
    public class FavouriteTweeterViewModel
    {
        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int StatusesCount { get; set; }

        public ICollection<TweetViewModel> Tweets { get; set; } = new List<TweetViewModel>();
    }
}
