using System.Collections.Generic;

namespace TwitterBackup.Web.Models.FavouriteTweetersViewModels
{
    public class FavouriteTweetersViewModel
    {
        public ICollection<FavouriteTweeterViewModel> FavouriteTweeters { get; set; }
    }
}
