namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface ICascadeDeleteEntityService
    {
		void CascadeDeleteUser(string userId);

		void CascadeDeleteFavouriteTweeter(string userId, string tweeterId);

		void CascadeDeleteTweet(string userId, string tweetId);
	}
}
