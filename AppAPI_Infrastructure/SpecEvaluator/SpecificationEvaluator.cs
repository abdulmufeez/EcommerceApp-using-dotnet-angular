using AppAPI_Core.Entities;
using AppAPI_Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace AppAPI_Infrastructure.SpecEvaluator
{
    public class SpecificationEvaluator<TEntity> where TEntity : BaseEntity
    {
        // evaluate db data with specification
        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery,
            ISpecification<TEntity> spec)
        {
            var query = inputQuery;

            if (spec.Condition is not null)
            {
                query = query.Where(spec.Condition);
            }

            if (spec.OderBy is not null)
            {
                query = query.OrderBy(spec.OderBy);
            }

            if (spec.OderByDesc is not null)
            {
                query = query.OrderByDescending(spec.OderByDesc);
            }

            // applyin one by one Icludes(x => x.Condition) to db data
            query = spec.Includes.Aggregate(query, (current, include) => current.Include(include));

            // return final results
            return query;
        }
    }
}