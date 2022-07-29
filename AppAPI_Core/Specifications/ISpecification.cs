using System.Linq.Expressions;

namespace AppAPI_Core.Specifications
{
    public interface ISpecification<T>
    { 
        // for filtering
        Expression<Func<T, bool>> Condition { get; }        
        List<Expression<Func<T, object>>> Includes { get; }

        // for sorting
        Expression<Func<T, object>> OderBy { get; }
        Expression<Func<T, object>> OderByDesc { get; }

        // for pagination
        int Take { get; }
        int Skip { get; }
        bool IsPagingEnabled { get; }
    }
}