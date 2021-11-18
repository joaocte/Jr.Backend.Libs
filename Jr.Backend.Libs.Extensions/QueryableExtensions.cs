using Jr.Backend.Libs.Extensions.Filter;
using Jr.Backend.Libs.Extensions.Pagination;
using Jr.Backend.Libs.Extensions.Sort;
using System.Linq;

namespace Jr.Backend.Libs.Extensions
{
    public static class QueryableExtensions
    {
        public static IQueryable<TEntity> Apply<TEntity>(this IQueryable<TEntity> result, ICustomQueryable model)
        {
            result = result.Filter(model);

            if (model is IQuerySort sort)
                result = result.Sort(sort);

            if (model is IQueryPaging pagination)
                result = result.Paginate(pagination);

            return result;
        }
    }
}