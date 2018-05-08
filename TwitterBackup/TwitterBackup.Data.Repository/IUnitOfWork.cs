using System.Threading.Tasks;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Repository
{
    public interface IUnitOfWork
    {
		IRepository<User> UsersRepository { get; }

		IRepository<Tweeter> TweeterRepository { get; }

		IRepository<Tweet> TweetRepository { get; }

		IRepository<UserTweet> UsersTweetRepository { get; }

		IRepository<UserTweeter> UsersTweeterRepository { get; }

		bool SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}
