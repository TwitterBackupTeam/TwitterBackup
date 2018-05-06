namespace TwitterBackup.Data.DTO.StatisticsDTOs
{
	public class UserStatisticsDTO
    {
		public string Id { get; set; }
		
		public int FollowersCount { get; set; }

		public string UserName { get; set; }

		public int StoredTweetsCount { get; set; }

		public int DeletedTweetsCount { get; set; }

		public int FavouriteTweetersCount { get; set; }

		public int DeletedFavouriteTweetersCount { get; set; }
	}
}
