using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Spacification
{
    public static class SpacificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery, ISpacification<TEntity> spec)
        {
            var Query = inputQuery; //_Context.Set<T>()
            if (spec.Predicate is not null)
            {
                Query = Query.Where(spec.Predicate); //_context.Set<T>().Where(Criteria)
            }
            Query = spec.Includes.Aggregate(Query, (CurrentQuery, IncludeExpression) => CurrentQuery.Include(IncludeExpression));
            return Query;
        }
    }
}
