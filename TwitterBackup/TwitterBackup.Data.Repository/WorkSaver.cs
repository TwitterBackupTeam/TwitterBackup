using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TwitterBackup.Data.Repository
{
    public class WorkSaver : IWorkSaver
    {
        private readonly DbContext dbContext;

        public WorkSaver(DbContext context)
        {
            this.dbContext = context;
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

                    throw;
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
