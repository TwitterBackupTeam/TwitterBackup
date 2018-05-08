using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using TwitterBackup.Data.Context;
using TwitterBackup.Data.Models.Abstract;

namespace TwitterBackup.Data.Repository
{
    public class EfRepository<T> : IRepository<T> where T : class, IDeletable
	{
        private TwitterBackupDbContext dbContext;
        private DbSet<T> dbSet;

        public EfRepository(TwitterBackupDbContext dbContext)
        {
			this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
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
			if (entity == null)
			{
				throw new ArgumentNullException("Entity cannot be null!");
			}

			entity.IsDeleted = true;
			entity.DeletedOn = DateTime.Now;

			var entry = this.dbContext.Entry(entity);

			if (entry.State != EntityState.Modified)
			{
				entry.State = EntityState.Modified;
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
