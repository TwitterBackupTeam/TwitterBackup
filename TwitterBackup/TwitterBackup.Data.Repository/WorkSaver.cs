using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using TwitterBackup.Data.Context;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Repository
{
	public class WorkSaver : IWorkSaver
    {
        private readonly TwitterBackupDbContext dbContext;

		public WorkSaver(TwitterBackupDbContext dbContext, IRepository<User> userRepository)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException("DbContext should not be null");
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
