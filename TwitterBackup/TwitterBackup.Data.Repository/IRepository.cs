using System.Linq;
using System.Threading.Tasks;

namespace TwitterBackup.Data.Repository
{
    public interface IRepository<T>
        where T : class
    {
        void Add(T entity);

        IQueryable<T> All(bool withoutCache = false);

        void Delete(T entity);

        void Delete(params object[] id);

        T GetById(params object[] id);

        void RefreshEntity(T entity);

        void Update(T entity);
    }
}
