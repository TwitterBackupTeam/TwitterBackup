using TwitterBackup.Data.Repository;
using TwitterBackup.Data.Services.Utils;

namespace TwitterBackup.Data.Services
{
    public abstract class DatabaseService
    {
        protected IAutoMapper AutoMapper { get; set; }
        protected IUnitOfWork UnitOfWork { get; set; }

        protected DatabaseService(IAutoMapper autoMapper, IUnitOfWork unitOfWork)
        {
            AutoMapper = autoMapper;
			UnitOfWork = unitOfWork;
        }
    }
}
