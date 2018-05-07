using System.Collections.Generic;

namespace TwitterBackup.Web.Models.AddFavouriteTweetsViewModels
{
    public class AddFavouriteTweetsViewModel
    {
        public AddFavouriteTweetsViewModel()
        {
            this.Tweets = new List<AddTweetViewModel>();
        }

        public AddTweeterViewModel TweeterViewModel { get; set; }

        public IList<AddTweetViewModel> Tweets { get; set; }
    }
}
