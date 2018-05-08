using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using TwitterBackup.Data.Context;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Repository
{
	public class UnitOfWork : IUnitOfWork
    {
        private readonly TwitterBackupDbContext dbContext;
		private IRepository<User> userRepository;
		private IRepository<UserTweeter> userTweeterRepository;
		private IRepository<UserTweet> userTweetRepository;
		private IRepository<Tweet> tweetRepository;
		private IRepository<Tweeter> tweeterRepository;
		public UnitOfWork(TwitterBackupDbContext dbContext, 
						  IRepository<User> userRepository, 
						  IRepository<UserTweeter> userTweeterRepository, 
						  IRepository<UserTweet> userTweetRepository, 
						  IRepository<Tweet> tweetRepository, 
						  IRepository<Tweeter> tweeterRepository)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException("DbContext should not be null");
			this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userTweeterRepository));
			this.userTweeterRepository = userTweeterRepository ?? throw new ArgumentNullException(nameof(userTweeterRepository));
			this.userTweetRepository = userTweetRepository ?? throw new ArgumentNullException(nameof(userTweetRepository));
			this.tweetRepository = tweetRepository ?? throw new ArgumentNullException(nameof(tweetRepository));
			this.tweeterRepository = tweeterRepository ?? throw new ArgumentNullException(nameof(tweeterRepository));
		}
		public IRepository<User> UsersRepository
		{
			get
			{
				if (this.userRepository == null)
				{
					this.userRepository = new EfRepository<User>(dbContext);
				}

				return this.userRepository;
			}
		}

		public IRepository<UserTweeter> UsersTweeterRepository
		{
			get
			{
				if (this.userTweeterRepository == null)
				{
					this.userTweeterRepository = new EfRepository<UserTweeter>(dbContext);
				}

				return this.userTweeterRepository;
			}
		}

		public IRepository<UserTweet> UsersTweetRepository
		{
			get
			{
				if (this.userTweetRepository == null)
				{
					this.userTweetRepository = new EfRepository<UserTweet>(dbContext);
				}

				return this.userTweetRepository;
			}
		}

		public IRepository<Tweet> TweetRepository
		{
			get
			{
				if (this.tweetRepository == null)
				{
					this.tweetRepository = new EfRepository<Tweet>(dbContext);
				}

				return this.tweetRepository;
			}
		}

		public IRepository<Tweeter> TweeterRepository
		{
			get
			{
				if (this.tweeterRepository == null)
				{
					this.tweeterRepository = new EfRepository<Tweeter>(dbContext);
				}

				return this.tweeterRepository;
			}
		}

		public bool SaveChanges()
		{
			int result;

			using (var dbContextTransaction = this.dbContext.Database.BeginTransaction())
			{
				try
				{
					result = this.dbContext.SaveChanges();
					dbContextTransaction.Commit();
				}
				catch (Exception ex)
				{
					dbContextTransaction.Rollback();
					
					throw new Exception(ex.Message);
				}
			}

			return result > 0;
		}

		public async Task<bool> SaveChangesAsync()
		{
			int result = 0;

            using (var dbContextTransaction = this.dbContext.Database.BeginTransaction())
            {
                try
                {
                    //await this.dbContext.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT Tweeters ON;");

                    //// Detach all tweets
                    //var tweetsThatNeedToBeUpdated = this.dbContext.ChangeTracker.Entries<Tweet>().Where(x =>
                    //    x.State != EntityState.Detached
                    //    && x.State != EntityState.Unchanged);

                    //this.dbContext.ChangeTracker.Entries<Tweet>().Select(e => e.State = EntityState.Detached);
                    //result = await this.dbContext.SaveChangesAsync(false);
                    //dbContextTransaction.Commit();

                    //await this.dbContext.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT Tweeters OFF; SET IDENTITY_INSERT Tweets ON;");
                    //this.dbContext.ChangeTracker.Entries<Tweet>().
                    //    Where(e => tweetsThatNeedToBeUpdated.Contains(e)).
                    //    Select(e => e.State = tweetsThatNeedToBeUpdated.First(en => en == e).State);
                    //result = await this.dbContext.SaveChangesAsync(false);
                    //dbContextTransaction.Commit();
                    //await this.dbContext.Database.ExecuteSqlCommandAsync("SET IDENTITY_INSERT Tweets OFF;");

                    result = await this.dbContext.SaveChangesAsync();
                    dbContextTransaction.Commit();
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();

                    if (ex is AggregateException)
                    {
                        // proper stack trace
                        ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
                    }

                    throw;
                }
            }

			return result > 0;
		}
	}
}
