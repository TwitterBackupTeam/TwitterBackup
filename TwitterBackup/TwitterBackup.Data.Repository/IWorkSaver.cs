using System.Threading.Tasks;
using TwitterBackup.Data.Models;

namespace TwitterBackup.Data.Repository
{
    public interface IWorkSaver
    {
		bool SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}
