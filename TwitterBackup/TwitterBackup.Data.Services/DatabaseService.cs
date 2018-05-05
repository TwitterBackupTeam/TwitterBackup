using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public abstract class DatabaseService
    {
        protected IAutoMapper AutoMapper { get; set; }
        protected IWorkSaver WorkSaver { get; set; }

        protected DatabaseService(IAutoMapper autoMapper, IWorkSaver workSaver)
        {
            AutoMapper = autoMapper;
            WorkSaver = workSaver;
        }
    }
}
