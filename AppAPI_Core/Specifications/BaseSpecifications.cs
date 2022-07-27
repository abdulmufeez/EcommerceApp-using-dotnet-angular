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

        // helper method for includes
        protected void AddInclude (Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
    }
}