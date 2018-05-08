using System;
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
            this.AutoMapper = autoMapper ?? throw new ArgumentNullException(nameof(autoMapper));

            this.UnitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(autoMapper));
            AutoMapper = autoMapper;
			UnitOfWork = unitOfWork;
        }
    }
}
