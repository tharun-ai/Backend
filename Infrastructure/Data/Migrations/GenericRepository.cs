using System.Linq.Expressions;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data.Migrations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity
    {

       
        public readonly StoreContext _context;
        private readonly DbSet<T> _db;
        public GenericRepository(StoreContext context)
        {
           _context=context;
           _db=context.Set<T>();
        }

        public async Task<T> GetByIDAsync(Expression<Func<T, bool>> expression, List<string> includes = null)
        {
            IQueryable<T> query=_db;
           if(includes!=null){
            foreach(var includeProperty in includes){
               query=query.Include(includeProperty);
            } 
           }

           return await query.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<IList<T>> ListAllSync(Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<string> includes = null)
        {
          IQueryable<T> query=_db;
          if(expression!=null){
            query=query.Where(expression);
          }
           if(includes!=null){
            foreach(var includeProperty in includes){
               query=query.Include(includeProperty);
            } 
           }
           if(orderBy!=null){
            query=orderBy(query);
           }
           return await query.AsNoTracking().ToListAsync();
        }

        public  void update(T Entity)
        {
             _db.Attach(Entity);
             _context.Entry(Entity).State=EntityState.Modified;
        }

        // public async Task<int> CountAsync(ISpecification<T> spec)
        // {
        //     return await ApplySpecification(spec).CountAsync();
        // }

        // public async Task<T> GetByIDAsync(int id)
        // {
        //    return await _context.Set<T>().FindAsync(id);
        // }

        // public async Task<T> GetEntityWithSpec(ISpecification<T> spec)
        // {
        //     return await ApplySpecification(spec).FirstOrDefaultAsync();
        // }

        // public async Task<IReadOnlyList<T>> ListAllSync()
        // {
        //     return await _context.Set<T>().ToListAsync();
        // }

        // public async Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec)
        // {
        //     return await ApplySpecification(spec).ToListAsync();
        // }



        // private IQueryable<T> ApplySpecification(ISpecification<T> spec){
        //     return SpecificationEvaluator<T>.GetQuery(_context.Set<T>().AsQueryable(),spec);
        // }
    }
}