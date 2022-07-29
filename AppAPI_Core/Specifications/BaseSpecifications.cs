using System.Linq.Expressions;

namespace AppAPI_Core.Specifications
{
    // this interface made for generalization when using dbcontext or making request to database
    public class BaseSpecification<T> : ISpecification<T>
    {
        public BaseSpecification()
        {
        }

        public BaseSpecification(Expression<Func<T, bool>> condition)
        {
            Condition = condition;
        }

        // conditon is like == where(x => x.id == id)
        public Expression<Func<T, bool>> Condition { get;}

        // includes is like == Inlcudes(x => x.type)
        public List<Expression<Func<T, object>>> Includes { get;} = 
            new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OderBy {get; private set; } 

        public Expression<Func<T, object>> OderByDesc {get; private set; }

        // helper method for includes
        protected void AddInclude (Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }

        protected void AddOrderBy (Expression<Func<T, object>> orderByExpression)
        {
            OderBy = orderByExpression;
        }

        protected void AddOrderByDesc (Expression<Func<T, object>> orderByDescExpression)
        {
            OderByDesc = orderByDescExpression;
        }
    }
}