using System.Threading.Tasks;

namespace TwitterBackup.Data.Repository
{
    public interface IWorkSaver
    {
        bool SaveChanges();

        Task<bool> SaveChangesAsync();
    }
}
