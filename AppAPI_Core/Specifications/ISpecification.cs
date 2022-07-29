using System.Linq.Expressions;

namespace AppAPI_Core.Specifications
{
    public interface ISpecification<T>
    { 
        Expression<Func<T, bool>> Condition { get; }        
        List<Expression<Func<T, object>>> Includes { get; }
        Expression<Func<T, object>> OderBy { get; }
        Expression<Func<T, object>> OderByDesc { get; }
    }
}