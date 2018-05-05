using System.Collections.Generic;
using System.Linq;

namespace TwitterBackup.Data.Services.Utils
{
    public interface IAutoMapper
    {
        T MapTo<T>(object source);

        IQueryable<T> ProjectTo<T>(IQueryable<object> source);

        IEnumerable<T> ProjectTo<T>(IEnumerable<object> source);
    }
}
