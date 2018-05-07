using System.Threading.Tasks;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Repository
{
    public interface IUnitOfWork
    {
		bool SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}
