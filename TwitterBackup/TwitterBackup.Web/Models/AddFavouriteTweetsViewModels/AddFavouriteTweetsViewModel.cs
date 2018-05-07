using System.Collections.Generic;
using TwitterBackup.Web.Models.FavouriteTweetersViewModels;

namespace TwitterBackup.Web.Models.AddFavouriteTweetsViewModels
{
    public class AddFavouriteTweetsViewModel
    {
        public AddFavouriteTweetsViewModel()
        {
            this.Tweets = new List<AddTweetViewModel>();
        }

        public TweeterViewModel TweeterViewModel { get; set; }

        public ICollection<AddTweetViewModel> Tweets { get; set; }
    }
}
