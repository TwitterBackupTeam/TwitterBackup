using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Collections.Generic;
using System.Linq;

namespace TwitterBackup.Data.Services.Utils
{
    public class AutoMapperWrapper : IAutoMapper
    {
        private readonly IMapper mapper;

        public AutoMapperWrapper(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public T MapTo<T>(object source)
        {
            return this.mapper.Map<T>(source);
        }

        public IQueryable<T> ProjectTo<T>(IQueryable<object> source)
        {
            return source.ProjectTo<T>();
        }

        public IEnumerable<TDestination> ProjectTo<TDestination>(IEnumerable<object> source)
        {
            return source.AsQueryable().ProjectTo<TDestination>();
        }
    }
}
