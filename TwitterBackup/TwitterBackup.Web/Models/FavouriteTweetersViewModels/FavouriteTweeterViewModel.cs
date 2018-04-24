using System.Collections.Generic;

namespace TwitterBackup.Web.Models.FavouriteTweetersViewModels
{
    public class FavouriteTweeterViewModel
    {
        public string Name { get; set; }
        public string ProfileImageUrl { get; set; }
        public ICollection<TweetViewModel> Tweets { get; set; }
    }
}
