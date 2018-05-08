namespace TwitterBackup.Data.DTO.UserManagementDTOs
{
	public class ListTweetersDTO
    {
		public long Id { get; set; }

		public string Name { get; set; }

		public string ScreenName { get; set; }

		public int FollowersCount { get; set; }
	}
}
