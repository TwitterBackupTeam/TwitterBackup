namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface ICascadeDeleteEntityService
    {
		void CascadeDeleteUser(string userId);

		void CascadeDeleteUserTweeter(string userId, long tweeterId);

		void CascadeDeleteTweet(string userId, long tweetId);
	}
}
