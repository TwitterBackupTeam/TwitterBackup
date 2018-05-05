namespace TwitterBackup.Web.Models.FavouriteTweetersViewModels
{
    public class TweetViewModel
    {
        public long Id { get; set; }

        public string Text { get; set; }
        
        public int RetweetCount { get; set; }

        public int FavouriteCount { get; set; }

    }
}
