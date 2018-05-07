using System.Collections.Generic;

namespace TwitterBackup.Web.Models.AddFavouriteTweetsViewModels
{
    public class AddTweeterViewModel
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public int FollowersCount { get; set; }

        public int FollowingCount { get; set; }

        public int StatusesCount { get; set; }

        public ICollection<AddTweetViewModel> Tweets { get; set; } = new List<AddTweetViewModel>();
    }
}
