using Microsoft.EntityFrameworkCore;
using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Repository
{
	public class WorkSaver : IWorkSaver
	{
		private readonly DbContext dbContext;
		private IRepository<User> userRepository;

		public WorkSaver(DbContext dbContext)
		{
			this.dbContext = dbContext ?? throw new ArgumentNullException("DbContext should not be null");
		}

		public WorkSaver(IRepository<User> userRepository)
		{
			this.userRepository = userRepository;
		}

		public IRepository<User> UserRepository
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
					result = await this.dbContext.SaveChangesAsync();
					dbContextTransaction.Commit();
				}
				catch (Exception ex)
				{
					dbContextTransaction.Rollback();

					// proper stack trace
					ExceptionDispatchInfo.Capture(ex.InnerException).Throw();
				}
			}

			return result > 0;
		}
	}
}
