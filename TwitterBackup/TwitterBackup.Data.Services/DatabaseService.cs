using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public abstract class DatabaseService
    {
        protected IAutoMapper AutoMapper { get; set; }
        protected IUnitOfWork WorkSaver { get; set; }

        protected DatabaseService(IAutoMapper autoMapper, IUnitOfWork workSaver)
        {
            AutoMapper = autoMapper;
            WorkSaver = workSaver;
        }
    }
}
