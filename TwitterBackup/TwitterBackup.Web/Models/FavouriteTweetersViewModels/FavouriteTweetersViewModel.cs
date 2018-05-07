using System.Collections.Generic;

namespace TwitterBackup.Web.Models.FavouriteTweetersViewModels
{
    public class FavouriteTweetersViewModel
    {
        public string SearchScreenName { get; set; }

        public IEnumerable<TweeterViewModel> TweeterViewModels { get; set; }
    }
}
