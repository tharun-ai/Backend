using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.Entities;
using Core.Specifications;

namespace Core.Interfaces
{
    public interface IGenericRepository<T> where T:BaseEntity
    {
        // Task<T> GetByIDAsync(int id);
        // Task<IReadOnlyList<T>> ListAllSync();
        // Task<T> GetEntityWithSpec(ISpecification<T> spec);
        // Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);

        // Task<int> CountAsync(ISpecification<T> spec);

        Task<IList<T>> ListAllSync(Expression<Func<T,bool>> expression=null,Func<IQueryable<T>,IOrderedQueryable<T>> orderBy=null,
        List<string> includes=null);

        Task<T> GetByIDAsync(Expression<Func<T,bool>> expression,List<string> includes=null);

       void  update(T Entity);



        
    }
}