namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class UserStatisticsDTO
    {
		public string Id { get; set; }
		
		public int FollowersCount { get; set; }

		public string ScreenName { get; set; }

		public int SavedTweetsCount { get; set; }

		public int DeletedTweetsCount { get; set; }

		public int FavouriteTweetrsCount { get; set; }

		public int DeletedFavouriteTweetrsCount { get; set; }
	}
}
