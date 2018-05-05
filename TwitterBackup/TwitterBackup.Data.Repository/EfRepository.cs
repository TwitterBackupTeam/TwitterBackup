using Microsoft.EntityFrameworkCore;
using System.Linq;
using TwitterBackup.Data.Context;

namespace TwitterBackup.Data.Repository
{
    public class EfRepository<T> : IRepository<T> where T : class
    {
        private TwitterBackupDbContext dbContext;
        private DbSet<T> dbSet;

        public EfRepository(TwitterBackupDbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = this.dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.dbSet.Add(entity);
            }
        }

        public IQueryable<T> All(bool withoutCache = false)
        {
            var query = this.dbSet.AsQueryable();

            if (withoutCache)
            {
                query = query.AsNoTracking();
            }

            return query;
        }

        public void Delete(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.dbSet.Attach(entity);
                this.dbSet.Remove(entity);
            }
        }

        public void Delete(params object[] id)
        {
            var entity = this.GetById(id);
            if (entity != null)
            {
                this.Delete(entity);
            }
        }

        public T GetById(params object[] id)
        {
            return this.dbSet.Find(id);
        }

        public void RefreshEntity(T entity)
        {
            this.dbContext.Entry(entity).Reload();
        }

        public void Update(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
