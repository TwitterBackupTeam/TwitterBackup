using TwitterBackup.Data.DTO;

namespace TwitterBackup.Data.Services.ServiceInterfaces
{
	public interface IUserTweeterService
	{
		bool DbContainsUserTweeter(string userId, long tweeterId);

		bool UserTweeterIsDeleted(string userId, long tweeterId);

	    bool SaveUserTweeter(string userId, long tweeterId);

        void SaveUserTweeter(string userId, TweeterDTO tweeter);

		void DeleteUserTweeter(string userId, long tweeterId);
	}
}
