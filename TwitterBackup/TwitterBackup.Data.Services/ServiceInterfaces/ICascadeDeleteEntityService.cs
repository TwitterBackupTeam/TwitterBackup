namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface ICascadeDeleteEntityService
    {
		void CascadeDeleteUser(string userId);

		void CascadeDeleteFavouriteTweeter(string userId, long tweeterId);

		void CascadeDeleteTweet(string userId, long tweetId);
	}
}
