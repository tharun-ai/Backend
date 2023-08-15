using System.Linq.Expressions;

namespace Core.Specifications
{
    public class BaseSpecification<T> : ISpecification<T>
    { 
         public BaseSpecification()
        {
            
        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }

        public Expression<Func<T, bool>> Criteria{get;}

        public List<Expression<Func<T, object>>> Includes{get;}=new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy{get;private set;}
        public Expression<Func<T, object>> OrderbyDesc{get;private set;}

        public int Take{get;private set;}

        public int Skip {get;private set;}

        public bool IsPagingEnabled {get;private set;}

        protected void AddInclude(Expression<Func<T,object>> includeExpressions){
            Includes.Add(includeExpressions);
        }

        protected void AddOrderBy(Expression<Func<T,object>> orderByExpressions){
            OrderBy = orderByExpressions;
        }

         protected void AddOrderByDescedingOrder(Expression<Func<T,object>> orderByExpressions){
            OrderbyDesc = orderByExpressions;
        }

        protected void AddingPaging(int take,int skip, bool isPagingEnabled=false){
            Take=take;
            Skip=skip;
            IsPagingEnabled=isPagingEnabled;
        }

        
    }
}